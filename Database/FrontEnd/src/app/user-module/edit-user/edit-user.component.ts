import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-edit-user',
  templateUrl: './edit-user.component.html',
  styleUrls: ['./edit-user.component.scss'],
})
export class EditUserComponent implements OnInit {

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
