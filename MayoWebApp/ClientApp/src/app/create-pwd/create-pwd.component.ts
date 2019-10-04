import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserRoleService } from '../Services/user-role.service';
import { userMaster, createPwd } from '../models/userModel';

@Component({
  selector: 'app-create-pwd',
  templateUrl: './create-pwd.component.html',
  styleUrls: ['./create-pwd.component.css']
})
export class CreatePwdComponent implements OnInit {

  IsLoginBtn: boolean = false;
  txtUserName: string;
  txtPassword: string;
  userId: string = "";
  IsSucess: boolean = false;
  IsError: boolean = false;
  errorMsg: string;
  createPwd: createPwd = new createPwd();
  //userModel: userMaster = new userMaster();

  constructor(private route: ActivatedRoute, private userRoleService: UserRoleService) { }

  ngOnInit() {
    if (String(localStorage.getItem("userId") == ""))
      this.userId = this.route.snapshot.params.userId;
    else
      this.userId = String(localStorage.getItem("userId"));
    localStorage.removeItem("userId");

    this.userRoleService.getUserbyId(this.userId).subscribe(
      result => { this.txtUserName = result.userName }
    );
  }

  onSavePwd() {
    this.createPwd.userId = this.userId;
    this.createPwd.password = this.txtPassword;

    this.userRoleService.createNewPwd(this.createPwd).subscribe(data => (this.IsSucess = true, this.IsLoginBtn = true),
      error => (this.errorMsg = error.error, this.IsError = true, setTimeout(() => { this.closeAlert(); }, 5000)));
  }

  closeAlert() {
    this.IsSucess = false;
    this.IsError = false;
    //this.router.navigate(['/login/']);
  }

}
