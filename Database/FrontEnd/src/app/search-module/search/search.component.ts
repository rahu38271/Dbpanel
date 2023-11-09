import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import * as xlsx from 'xlsx';
import { DatasearchService } from 'src/app/services/datasearch.service';
import { LoaderService } from 'src/app/services/loader.service';
import { MenuController } from '@ionic/angular';


@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss'],
})
export class SearchComponent implements OnInit {
  isShow=false;
  @ViewChild('epltable', { static: false }) epltable: ElementRef;
  mobileNoData:any;
  altMobData:any;
  nameData:any;
  addressData:any;
  nameAdrsData:any;
  adrsPinData:any;
  pinDataCount:any;
  namePinCountData:any;
  mobileModal:any={}
  nameModal:any={};
  altMobModal:any={};
  adrsModal:any={};
  nameAdrsModal:any={};
  adrsPinModal:any={};
  PinModal:any={};
  namePinModal:any={};
  PinCode:any;
  imageUrl:string='';
  fileType:any;
  Name:any;
  search = '';

  mobile = '';

  constructor(
    private dataSearch:DatasearchService,
    private loader:LoaderService,
    private menuCtrl: MenuController
  ) { }

  ngOnInit() {
    this.namePinCount();
   }

  ionViewWillEnter(){
    this.menuCtrl.enable(true);
  }

  searchMob(){
    this.loader.showLoader();
    this.dataSearch.searchWithMobile(this.mobileModal.MobileNo).subscribe(data=>{
      if(data){
        this.loader.hideLoader();
        this.mobileNoData = data;
        this.mobileNoData.forEach(e => {
          e.birthDate = e.birthDate.split('T')[0];
        });
      }
      else{
        this.loader.hideLoader();
      }
    },(err)=>{
      this.loader.hideLoader();
    })
  }

  searchName(){
    this.loader.showLoader();
    this.dataSearch.searchWithName(this.nameModal.Name).subscribe(data=>{
      if(data){
        this.nameData = data;
        this.loader.hideLoader();
      }
      else{
        this.loader.hideLoader();
      }
    },(err)=>{
      this.loader.hideLoader();
    })
  }

  searchAltMob(){
    this.loader.showLoader();
    this.dataSearch.searchWithAltMob(this.altMobModal.AltMobileNo).subscribe(data=>{
      if(data){
        this.loader.hideLoader();
        this.altMobData = data;
      }
      else{
        this.loader.hideLoader();
      }
    },(err)=>{
      this.loader.hideLoader();
    })
  }

  searchAdrs(){
    this.loader.showLoader();
    this.dataSearch.searchWithAddress(this.adrsModal.Address).subscribe(data=>{
      if(data){
        this.loader.hideLoader();
        this.addressData = data;
      }
      else{
        this.loader.hideLoader();
      }
    },(err)=>{
      this.loader.hideLoader();
    })
  }

  searchNameAdrs(){
    this.loader.showLoader();
    this.dataSearch.searchWithNameAddress(this.nameAdrsModal.Address,this.nameAdrsModal.Name).subscribe(data=>{
      if(data){
        this.loader.hideLoader();
        this.nameAdrsData = data;
      }
      else{
        this.loader.hideLoader();
      }
    },(err)=>{
      this.loader.hideLoader();
    })
  }

  searchAdrsPin(){
    this.loader.showLoader();
    this.dataSearch.searchWithAdrsPincode(this.adrsPinModal.Address,this.adrsPinModal.Pincode).subscribe(data=>{
      if(data){
        this.loader.hideLoader();
        this.adrsPinData = data;
      }
      else{
        this.loader.hideLoader();
      }
    },(err)=>{
      this.loader.hideLoader();
    })
  }

  searchPinCode(){
    this.loader.showLoader();
    this.dataSearch.getPinCodeCount(this.PinModal.PinCode).subscribe(data=>{
      if(data){
        this.loader.hideLoader();
        this.pinDataCount = data;
      }
      else{
        this.loader.hideLoader();
      }
    },(err)=>{
      this.loader.hideLoader();
    })
  }

  downloadPinData(){
    this.loader.showLoader();
    this.PinCode = this.PinModal.PinCode;
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
    link.download = this.PinModal.PinCode + '.csv';
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

  exportToExcel() {
    const ws: xlsx.WorkSheet =
      xlsx.utils.table_to_sheet(this.epltable.nativeElement);
    const wb: xlsx.WorkBook = xlsx.utils.book_new();
    xlsx.utils.book_append_sheet(wb, ws, 'Sheet1');
    xlsx.writeFile(wb, 'epltable.xlsx');
  }

  keyPressNumbers(event) {
    var charCode = (event.which) ? event.which : event.keyCode;
    // Only Numbers 0-9
    if ((charCode < 48 || charCode > 57)) {
      event.preventDefault();
      return false;
    } else {
      return true;
    }
  }

  letterOnly(event) {
    var charCode = (event.which) ? event.which : event.keyCode;
    // Only Numbers 0-9
    if ((charCode > 48 && charCode < 57)) {
      event.preventDefault();
      return false;
    } else {
      return true;
    }
  }

  namePinCount(){
    this.loader.showLoader();
    this.PinCode = this.namePinModal.PinCode;
    this.Name = this.namePinModal.Name;
    this.dataSearch.getNamePinCount(this.namePinModal.PinCode,this.namePinModal.Name).subscribe(data=>{
      if(data){
        this.loader.hideLoader();
        this.namePinCountData = data;
        this.isShow = !this.isShow;
      }
      else{
        this.loader.hideLoader();
      }
    },(err)=>{
      this.loader.hideLoader();
    })
  }

  saveNamePinFile(imageData: Blob) {
    const blob = new Blob([imageData], { type: 'text/csv' });
    const link = document.createElement('a');
    link.href = window.URL.createObjectURL(blob);
    // link.download = '';
    link.download = 'namePincode.csv';
    link.click();
  }

  downloadNamePin(){
    this.loader.showLoader();
    this.PinCode = this.namePinModal.PinCode;
    this.Name = this.namePinModal.Name;
    this.dataSearch.getNamePinFile(this.PinCode,this.Name).subscribe((data:Blob)=>{
      if(data.size!=0){
        this.loader.hideLoader();
        this.fileType = data.type;
        this.saveNamePinFile(data);
        this.fetchImage(data);
      }
      else{
        this.loader.hideLoader();
      }
    },(err)=>{
      this.loader.hideLoader();
    })
  }

}
