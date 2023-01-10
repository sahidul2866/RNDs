import { Component,Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-report',
  templateUrl: './report.component.html',
  styleUrls: ['./report.component.css']
})
export class ReportComponent {
  json:any = '';
  down:any;
  constructor(private http: HttpClient) {

  }

  onSubmit() {
    //console.log('ok');
    const a = JSON.parse(this.json);
    //console.log(JSON.stringify(a,null,2));
    const url = 'http://127.0.0.1:5000';
    this.http.post<any>(url,a ,{ responseType: 'blob' as 'json'}).subscribe( 
      (response: any)=>{
        let dataType = response.type;
        let binaryData = [];
        binaryData.push(response);
        let downloadLink = document.createElement('a');
        downloadLink.href = window.URL.createObjectURL(new Blob(binaryData, {type: dataType}));
        downloadLink.setAttribute('download', 'test.docx');
        document.body.appendChild(downloadLink);
        downloadLink.click();
    })
    
     
    //console.log(this.down);
    
  }


}
