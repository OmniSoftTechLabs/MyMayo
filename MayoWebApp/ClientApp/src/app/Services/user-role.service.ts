import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { userMaster } from '../models/userModel';

@Injectable({
  providedIn: 'root'
})
export class UserRoleService {
  baseUrl: string;
  private headers: HttpHeaders;


  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
    this.headers = new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8' });
  }

  addUserMaster(userModel: userMaster) {
    return this.http.post<userMaster>(this.baseUrl + 'api/UserMasters', userModel);

    //return this.http.post<userMaster>(this.baseUrl + 'api/UserMasters', userModel).subscribe(result => {
    //  this.userM = result;
    //}, error => console.error(error));
  }

  addTeacherUser(userModel: userMaster) {
    return this.http.post<userMaster>(this.baseUrl + 'api/UserMasters/AddTeacherUserMaster', userModel);
  }

  getAllUser() {
    return this.http.get<userMaster[]>(this.baseUrl + 'api/UserMasters/GetUserMaster');

    //  .subscribe(result => {
    //  this.userList = result;
    //}, error => console.error(error));
  }
}
