import { Component, OnInit } from '@angular/core';
import { DatasearchService } from 'src/app/services/datasearch.service';
import { LoaderService } from 'src/app/services/loader.service';

@Component({
  selector: 'app-multipin',
  templateUrl: './multipin.component.html',
  styleUrls: ['./multipin.component.scss'],
})
export class MultipinComponent implements OnInit {
  isShow=true;
  PinCode:any;
  pinCode:any;
  pinCodesFile:any;
  pinModal:any={
    PinCode:'',
  }

  imageUrl:string='';
  fileType:any;

  constructor(
    private dataSearch:DatasearchService,
    private loader:LoaderService
  ) { }

  ngOnInit() {}

  addPinCode(){
    this.pinModal.PinCode = this.pinModal.PinCode;
    this.dataSearch.getMultiPincountFile(this.pinModal.PinCode).subscribe(data=>{
      if(data){
        this.pinCodesFile=data;
        this.isShow=false;
      }
    })
  }

  pincodeData(pinCode){
    this.pinCode = pinCode;
    this.PinCode=pinCode;
    this.dataSearch.getPincodeFile(this.PinCode).subscribe((data:Blob)=>{
      if(data.size!=0){
        this.loader.hideLoader();
        this.fileType = data.type;
        this.saveFile(data);
        this.fetchImage(data);
      }
      else{
        this.loader.hideLoader();
      }
    },(err)=>{
      this.loader.hideLoader();
    })
  }

  saveFile(imageData: Blob) {
    const blob = new Blob([imageData], { type: 'text/csv' });
    const link = document.createElement('a');
    link.href = window.URL.createObjectURL(blob);
    // link.download = '';
    link.download = this.pinCode + '.csv';
    link.click();
  }

  // to download image from get api
  fetchImage(image:Blob){
    const reader = new FileReader();
    reader.onload = ()=>{
      this.imageUrl = reader.result as string;
    };
    reader.readAsDataURL(image);
  }

}
