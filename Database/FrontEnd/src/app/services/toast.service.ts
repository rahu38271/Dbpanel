import { Injectable } from '@angular/core';
import { ToastController } from '@ionic/angular';

@Injectable({
  providedIn: 'root'
})
export class ToastService {

  constructor(
    private toast: ToastController
  ) { }

  async showToast(message: string, color:string) {
    const toast = await this.toast.create({
      message: message,
      duration:1000,
      color:color,
      position: 'top' // You can change the position as needed
    });
    toast.present();
  }

  async hideToast(){
    this.toast.dismiss();
  }

}
