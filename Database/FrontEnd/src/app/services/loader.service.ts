import { Injectable } from '@angular/core';
import { LoadingController } from '@ionic/angular';

@Injectable({
  providedIn: 'root'
})
export class LoaderService {

  isLoading:any;

  constructor(
    public loadingController: LoadingController
  ) { }

  async showLoader() {
    this.isLoading=true;
    return await this.loadingController.create({
      cssClass: 'my-custom-class',
      message: 'Please wait...',
    }).then(a => {
      a.present().then(() => {
        console.log('');
        if (!this.isLoading) {
          a.dismiss().then(() => console.log(''));
        }
      });
    });
    
  }

  async hideLoader(){
    this.isLoading = false;
    return await this.loadingController.dismiss();
  }
}
