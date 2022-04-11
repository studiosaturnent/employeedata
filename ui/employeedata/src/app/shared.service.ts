import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';\
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SharedService {
readonly APIUrl="https://localhost:44356/"
readonly PhotoUrl = "https://localhost:44356/Photos"

  constructor(private http:HttpClient) { }

    //Department Module Method

  getDepList():Observable<any[]>{
    return this.http.get<any>(this.APIUrl+'/department')
  }

  addDepartment(val:any[]){
    return this.http.get<any>(this.APIUrl+'/Department')
  }

  updateDepartment(val:any[]){
    return this.http.get<any>(this.APIUrl+'/Department')
  }

  deleteDepartment(val:any[]){
    return this.http.get<any>(this.APIUrl+'/Department')
  }

  //Employee Module Method
  getEmpList():Observable<any[]>{
    return this.http.get<any>(this.APIUrl+'/Employee')
  }
  
  addEmployee(val:any[]){
    return this.http.get<any>(this.APIUrl+'/Employee')
  }

  updateEmployee(val:any[]){
    return this.http.get<any>(this.APIUrl+'/Employee')
  }

  deleteEmployee(val:any[]){
    return this.http.get<any>(this.APIUrl+'/Employee')
  }

  UploadPhoto(val:any){
    return this.http.post(this.APIUrl+'/Employee/SaveFile',val)
  }

  getAllDepartmentNames():Observable<any[]>{
    return this.http.get<any[]>(this.APIUrl+'/Employee')
  }


}
