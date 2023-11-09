import { Injectable } from '@angular/core';
import {HttpClient } from '@angular/common/http'


@Injectable({
  providedIn: 'root'
})
export class VoterdataService {
  url = "https://jsonplaceholder.typicode.com/posts"
  constructor(private http:HttpClient) { }
  getVoterData(){
    return this.http.get(this.url)
  }
}
