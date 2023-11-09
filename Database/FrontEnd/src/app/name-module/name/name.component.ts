import { Component, OnInit } from '@angular/core';
import { DatasearchService } from 'src/app/services/datasearch.service';
import * as  XLSX from 'xlsx';
import { Router } from '@angular/router';
import { LoaderService } from 'src/app/services/loader.service';
import { ToastService } from 'src/app/services/toast.service';

@Component({
  selector: 'app-name',
  templateUrl: './name.component.html',
  styleUrls: ['./name.component.scss'],
})
export class NameComponent implements OnInit {
  file:any;
  arraylist:any;
  nameMobCount:any;
  matchedNameMob:any;
  District:any;
  Name:any;
  MobileNo:any;
  //disabled:boolean= true;
  obj: any = {};
  excelUploadedData:any=[];
  searchData:any={
    District:'',
    Type:''
  }
  fileType:any;
  imageUrl:string='';
  allDistrict:any;

  constructor(
    private dataSearch:DatasearchService,
    private router:Router,
    private loader:LoaderService,
    private toast:ToastService
  ) { }

  ngOnInit() {
    this.getUplodedNameMobCount();
    this.showMatchedCount();
    this.districtList();
  }

  districtList(){
    this.dataSearch.getAllDistrict().subscribe(data=>{
      this.allDistrict = data;
    })
  }

  onFileSelected(event:any){
    if (event.target.files.length > 0) {
      this.file = event.target.files[0];
      this.ReadExcelData();
      if (this.file.type != "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet") {
        {
          // this.myFileInput.nativeElement.value = '';
          // return this.alertify.error("This file format is not allowed.");
          alert("This file format is not allowed.");
        }
        //this.ReadExcelData();
      }
    }
  }

  ReadExcelData() {
    debugger;
    var reader = new FileReader();
    var data = reader.result;
    reader.readAsBinaryString(this.file);
    reader.onload = (event) => {
      var data = reader.result;
      //var workbook = XLSX.read(data, { type: 'binary', cellDates: true });
      var workbook = XLSX.read(data, { type: 'binary' });
      var first_sheet_name = workbook.SheetNames[0];
      var worksheet = workbook.Sheets[first_sheet_name];
      //console.log(XLSX.utils.sheet_to_json(worksheet, { raw: true }));
      this.arraylist = XLSX.utils.sheet_to_json(worksheet, { raw: true });
      
      if (this.arraylist.length > 0) {
        // this.spinner.show(); 
        this.obj = (this.arraylist)[0];
        if (Object.keys(this.obj)[0] != "Name" ||
        (this.obj)[0] != "MobileNo" ) {
          //alert("Only provided template file is allowed. Please download the provided template only!")
          //this.disabled = true;
        }
        else {
          //this.disabled = false;
        }
      }
      else {
        alert("List is Empty!")
        //this.disabled = true; 
      }
      
      //arraylist = arraylist.map((u: any) => ({ value: u.vin }));
      if (this.arraylist.length > 0) {
        for (var i = 0; i < this.arraylist.length; i++) {

          if(this.arraylist[i].MobileNo = this.arraylist[i].MobileNo){
            this.arraylist[i].MobileNo = this.arraylist[i].MobileNo.toString();
          }

          var obj = {
            Name: this.arraylist[i].Name,
            MobileNo: this.arraylist[i].MobileNo,
          }
          console.log(this.excelUploadedData);
          this.excelUploadedData.push(obj);
       
        }
      }
      //this.excelUploadedData = arraylist;
    };
    reader.onerror = function (event) { console.error("File could not be read! Code "); };
  }

  upload(){
    this.loader.showLoader();
    this.dataSearch.searchInDatabase(this.excelUploadedData).subscribe(data=>{
      if(data){
        this.loader.hideLoader();
        this.ngOnInit();
        //this.router.navigate(['/search']);
        //alert("File uploaded succesfully!")
        //window.location.reload();
        this.toast.showToast("File uploaded succesfully!","success");
      }
      else{
        this.loader.hideLoader();
        this.toast.hideToast();
      }
    },(err)=>{
      this.loader.hideLoader();
      this.toast.hideToast();
    })
  }

  getUplodedNameMobCount(){
    this.dataSearch.uploadedNameMobCount().subscribe(data=>{
      if(data){
        this.nameMobCount = data;
      }
    })
  }

  searchFile(){
    //this.District="Aurangabad",
    this.loader.showLoader();
    if(this.searchData.Type=="Name"){
      this.Name="Y"
    }
    if(this.searchData.Type=="Mobile"){
      this.MobileNo="Y"
    }
    this.dataSearch.getMatchedDataFile(this.District,this.Name,this.MobileNo).subscribe((data:Blob)=>{
      if(data.size!=0){
        this.loader.hideLoader();
        this.fileType = data.type;
        this.saveFile(data);
        this.fetchImage(data);
        this.toast.showToast("File downloaded succesfully", "success")
      }
      else{
        this.loader.hideLoader();
      }
    },(err)=>{
      this.loader.hideLoader();
    })
  }

  showMatchedCount(){
    this.District = this.searchData.District;
    this.searchData.Type=this.searchData.Type;
    if(this.searchData.Type=="Name"){
      this.Name="Y"
    }
    else{
      this.Name=null
    }
    if(this.searchData.Type=="Mobile"){
      this.MobileNo="Y"
    }
    else{
      this.MobileNo=null
    }
 
    this.dataSearch.matchedNameMobCount(this.District,this.Name,this.MobileNo).subscribe(data=>{
      if(data){
        this.matchedNameMob = data;
      }
    })
  }



  saveFile(imageData: Blob) {
    const blob = new Blob([imageData], { type: 'text/csv' });
    const link = document.createElement('a');
    link.href = window.URL.createObjectURL(blob);
    // link.download = '';
    link.download = 'matchedData.csv';
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
