import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IonicModule } from '@ionic/angular';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { BulkUploadComponent } from './bulk-upload/bulk-upload.component';

@NgModule({
  declarations: [BulkUploadComponent],
  imports: [
    CommonModule,
    IonicModule,
    FormsModule,
    RouterModule.forChild([{path:'', component:BulkUploadComponent}])
  ]
})
export class BulkModuleModule { }
