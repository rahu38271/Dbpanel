import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MenuController } from '@ionic/angular';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {

  loginModal:any={
    Contact:'',
    Password:''
  }

  constructor(
    private router: Router,
    private menuCtrl:MenuController,
    private auth:AuthenticationService
  ) { }

  ngOnInit() {}

  login(){
    debugger;
    this.auth.loginUser(this.loginModal.Contact,this.loginModal.Password).subscribe(data=>{
      if(data){
        this.router.navigateByUrl('/search');
      }
      
    },(err)=>{
      alert("Username or Password incorrect.");
    })
    
  }

  ionViewWillEnter(){
    this.menuCtrl.enable(false);
  }

}
