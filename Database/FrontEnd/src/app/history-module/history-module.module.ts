import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IonicModule } from '@ionic/angular';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { HistoryComponent } from './history/history.component';


@NgModule({
  declarations: [HistoryComponent],
  imports: [
    CommonModule,
    IonicModule,
    FormsModule,
    RouterModule.forChild([{path:'', component:HistoryComponent}])
  ]
})
export class HistoryModuleModule { }
