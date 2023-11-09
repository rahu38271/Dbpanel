import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IonicModule } from '@ionic/angular';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { UserComponent } from './user/user.component';
import { AddUserComponent } from './add-user/add-user.component';
import { EditUserComponent } from './edit-user/edit-user.component';
import { ViewUserComponent } from './view-user/view-user.component';


@NgModule({
  declarations: [UserComponent, AddUserComponent, EditUserComponent, ViewUserComponent],
  imports: [
    CommonModule,
    IonicModule,
    FormsModule,
    RouterModule.forChild([{path:'', component:UserComponent}, {path:'add-user', component:AddUserComponent}, {path:'edit-user', component:EditUserComponent}, {path:'view-user', component:ViewUserComponent}])
  ]
})
export class UserModuleModule { }
