import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IonicModule } from '@ionic/angular';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { PermissionComponent } from './permission/permission.component';



@NgModule({
  declarations: [PermissionComponent],
  imports: [
    CommonModule,
    IonicModule,
    FormsModule,
    RouterModule.forChild([{path:'', component: PermissionComponent}])
  ]
})
export class PermissionModuleModule { }
