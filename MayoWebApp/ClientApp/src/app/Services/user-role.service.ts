import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { userMaster, createPwd } from '../models/userModel';
import { roleModel } from '../models/roleModel';

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
    return this.http.post<userMaster>(this.baseUrl + 'api/UserMasters/AddUserMaster', userModel);

    //return this.http.post<userMaster>(this.baseUrl + 'api/UserMasters', userModel).subscribe(result => {
    //  this.userM = result;
    //}, error => console.error(error));
  }

  addTeacherUser(userModel: userMaster) {
    return this.http.post<userMaster>(this.baseUrl + 'api/UserMasters/TeacherRegistration', userModel);
  }

  addAdminUser(userModel: userMaster) {
    return this.http.post<userMaster>(this.baseUrl + 'api/UserMasters/CreateAdminUser', userModel);
  }

  addStudentUser(userModel: userMaster) {
    return this.http.post<userMaster>(this.baseUrl + 'api/UserMasters/CreateStudentUser', userModel);
  }

  getUserbyId(guid: string) {
    return this.http.get<userMaster>(this.baseUrl + 'api/UserMasters/' + guid);
  }

  getAllUser() {
    return this.http.get<userMaster[]>(this.baseUrl + 'api/UserMasters/GetUserMaster');
  }

  loadRolesList() {
    return this.http.get<roleModel[]>(this.baseUrl + 'api/RoleMasters/GetRoleMaster');
  }

  createNewPwd(createPwd: createPwd) {
    return this.http.put<string>(this.baseUrl + 'api/UserMasters/CreateNewPassword', createPwd);
  }
  //  .subscribe(result => {
  //  this.userList = result;
  //}, error => console.error(error));

}
