import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  url = environment.apiUrl;

  //http://192.168.0.107:8012/api/Login/LoginUser?Contact=7412859623&Password=12345

  constructor(
    private http:HttpClient
  ) { }

  loginUser(Contact:any, Password:any){
    debugger;
    return this.http.get<any>(this.url+'Login/LoginUser?Contact='+Contact+'&Password='+Password)
  }
}
