import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DatasearchService {

  url = environment.apiUrl;

  constructor(
    private http:HttpClient
  ) { }

  // search with mobile
  searchWithMobile(MobileNo:any){
    return this.http.get<any>(this.url+'MobileSearch/SearchMobileNo?MobileNo='+MobileNo)
  }

  // search with name
  searchWithName(Name:any){
    return this.http.get<any>(this.url+'MobileSearch/SearchBy_Name?Name='+Name)
  }

  // search with alt mobile no.
  searchWithAltMob(AltMobileNo:any){
    return this.http.get<any>(this.url+'MobileSearch/SearchbyAltMobileNo?AltMobileNo='+AltMobileNo)
  }

  // search with address
  searchWithAddress(Address:any){
    return this.http.get<any>(this.url+'MobileSearch/Search_by_Address?Address='+Address)
  }

  // search with name and address
  searchWithNameAddress(Address:any, Name:any ){
    return this.http.get<any>(this.url+'MobileSearch/SearchBy_Address_Name?Address='+Address+'&Name='+Name)
  }

  // search with address and pincode
  searchWithAdrsPincode(Address:any, PinCode:any){
    return this.http.get<any>(this.url+'MobileSearch/SearchBy_Address_PinCode?Address='+Address+'&PinCode='+PinCode)
  }

  // search pincode 
  getPinCodeCount(PinCode:any){
    return this.http.get<any>(this.url+'MobileSearch/GetCountbyPinCode?PinCode='+PinCode)
  }

  // download pincode file
  getPincodeFile(PinCode:any):Observable<any>{
    return this.http.get(this.url+'MobileSearch/SearchBy_PinCode?PinCode='+PinCode,{ responseType:"text", reportProgress:true})
  }

  // bulk upload all data
  bulkUpload(bulkModal:any){
    debugger;
    return this.http.post<any>(this.url+'MobileSearch/AddBulkCustomer',bulkModal)
  }

  // upload name or mobile data to match data in database
  searchInDatabase(searchInDBModal:any){
    return this.http.post<any>(this.url+'CustomerName/InsertBulkCustomerName', searchInDBModal)
  }

  // uploaded name or mobile count
  uploadedNameMobCount(){
    return this.http.get(this.url+'CustomerName/GetCustomerNameCount')
  }

  matchedNameMobCount(District:any, Name:any, MobileNo:any){
    return this.http.get(this.url+'CustomerName/GetMobileMatchCount?District='+District+'&Name='+Name+'&MobileNo='+MobileNo)
  }
  

  // get matchedDataFile
  getMatchedDataFile(District:any, Name:any, MobileNo:any):Observable<any>{
    return this.http.get(this.url+'CustomerName/GetMobileMatch?District='+District+'&Name='+Name+'&MobileNo='+MobileNo,{ responseType:"text", reportProgress:true})
  }

  // get multiple pincode count and file
  getMultiPincountFile(PinCode:any){
    return this.http.get<any>(this.url+'MobileSearch/GetPincodewiseCount?PinCode='+PinCode)
  }

  //get district list
  getAllDistrict(){
    return this.http.get<any>(this.url+'MobileSearch/GetAllDistrict')
  }

  // get pincode and name count
  getNamePinCount(PinCode:any,Name:any){
    return this.http.get<any>(this.url+'MobileSearch/GetCount_SearchByPincodeName?PinCode='+PinCode+'&Name='+Name)
  }

  //get pincode and name file to download

  getNamePinFile(PinCode:any, Name:any):Observable<any>{
    debugger;
    return this.http.get(this.url+'MobileSearch/SearchByPincodeName?PinCode='+PinCode+'&Name='+Name,{ responseType:"text", reportProgress:true})
  }

}
