import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-add-user',
  templateUrl: './add-user.component.html',
  styleUrls: ['./add-user.component.scss'],
})
export class AddUserComponent implements OnInit {

  ismyTextFieldType: boolean;
  ismyTextFieldType1: boolean;

  constructor() { }

  ngOnInit() {}

  togglemyPasswordFieldType(){
    this.ismyTextFieldType = !this.ismyTextFieldType;
  }

  togglemyPasswordFieldType1(){
    this.ismyTextFieldType1 = !this.ismyTextFieldType1;
  }

}
