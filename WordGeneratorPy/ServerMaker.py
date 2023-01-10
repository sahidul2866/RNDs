from flask import Flask, request
import flask_cors
from flask_cors import CORS

import open_template

app = Flask(__name__)
CORS(app)

@app.route("/", methods = ['GET', 'POST', 'DELETE'])
def home():
    print(request.get_json())
    json = request.get_json()
    return open_template.callable_from_others(json["coverPageDict"], json["sectionData"], json["detailedIssueTableContent"], json["graph"])


if __name__ == "__main__":
    app.run(host="0.0.0.0", debug=True)