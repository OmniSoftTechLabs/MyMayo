import { Component, OnInit } from '@angular/core';
import { roleModel } from '../models/roleModel';
import { UserRoleService } from '../Services/user-role.service';
import { userMaster } from '../models/userModel';


@Component({
  selector: 'app-user-master',
  templateUrl: './user-master.component.html',
  styleUrls: ['./user-master.component.css']
})
export class UserMasterComponent implements OnInit {

  roleList: roleModel[] = [];
  userModel: userMaster = new userMaster();


  constructor(private userRoleService: UserRoleService) {
  }

  ngOnInit() {
    this.fillRoles();
    this.userModel.qualification = "";
    this.userModel.roleId = 0;
  }

  fillRoles() {
    this.userRoleService.loadRolesList().subscribe(
      roles => {
        this.roleList = roles;
      }, error => console.error(error));
  }

  onSaveUser() {
    this.userModel.userName = this.userModel.email;
    this.userRoleService.addUserMaster(this.userModel).subscribe(result => {
      this.userModel = result;
    }, error => console.error(error));

  }
}
