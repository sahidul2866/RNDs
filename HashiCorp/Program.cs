using System;
using System.Collections.Generic;
using VaultSharp;
using VaultSharp.V1.AuthMethods;
using VaultSharp.V1.AuthMethods.AppRole;
using VaultSharp.V1.AuthMethods.Token;
using VaultSharp.V1.Commons;
using VaultSharp.V1.SecretsEngines;

namespace DeveloperQuickstart
{
    class Program
    {
        static IVaultClient AuthenticateViaToken(string vaultAddr, string token)
        {
            //Authenticate
            IAuthMethodInfo authMethod = new TokenAuthMethodInfo(vaultToken: token);

            VaultClientSettings vaultClientSettings = new VaultClientSettings(vaultAddr, authMethod);
            IVaultClient vaultClient = new VaultClient(vaultClientSettings);
            return vaultClient;
        }

        static IVaultClient AuthenticateViaAppRole(string vaultAddr)
        {
            var roleId = "f83c2996-8101-f208-41d8-90d344c2bb55";
            var secretId = "ba1092a9-bfc4-6b20-ca09-ed95459c8a0c"; // Grab the secret_id

            // We create a second VaultClient and initialize it with the AppRole auth method and our new credentials.
            IAuthMethodInfo authMethod = new AppRoleAuthMethodInfo(roleId, secretId);
            var vaultClientSettings = new VaultClientSettings(vaultAddr, authMethod);

            IVaultClient vaultClient = new VaultClient(vaultClientSettings);
            return vaultClient;
        }

        static async Task getDBCred(IVaultClient vaultClient)
        {
            Secret<UsernamePasswordCredentials> dynamicDatabaseCredentials =
               await vaultClient.V1.Secrets.Database.GetCredentialsAsync(
                 "my-role",
                 "/my-secret-password/secrets");

            var userID = dynamicDatabaseCredentials.Data.Username;
            var password = dynamicDatabaseCredentials.Data.Password;
            Console.WriteLine(password);
        }

        static void Main(string[] args)
        {

            //authenticate via approle
            var vaultAddr = "http://127.0.0.1:8200";

            //var vaultClient = AuthenticateViaToken(vaultAddr, "root");
            var vaultClient = AuthenticateViaAppRole(vaultAddr);

            // Write a secret
           var secretData = new Dictionary<string, object> { { "password", "oLdViCtOrY2008" } };
            vaultClient.V1.Secrets.KeyValue.V2.WriteSecretAsync(
                path: "/my-secret-password",
                data: secretData,
                mountPoint: "secret"
            ).Wait();

            Console.WriteLine("Secret written successfully.");

            // Read a secret
            Secret<SecretData> secret = vaultClient.V1.Secrets.KeyValue.V2.ReadSecretAsync(
               path: "/my-secret-password",
                mountPoint: "secret"
            ).Result;

            getDBCred(vaultClient);

            var password = secret.Data.Data["password"];
            Console.WriteLine(password);

            if (password.ToString() != "oLdViCtOrY2008")
            {
                throw new System.Exception("Unexpected password");
            }

            Console.WriteLine("Access granted!");
        }
    }
}