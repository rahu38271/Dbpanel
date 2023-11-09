import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IonicModule } from '@ionic/angular';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { MultipinComponent } from './multipin/multipin.component';


@NgModule({
  declarations: [MultipinComponent],
  imports: [
    CommonModule,
    IonicModule,
    FormsModule,
    RouterModule.forChild([{path:'', component:MultipinComponent}])
  ]
})
export class MultipinModuleModule { }
