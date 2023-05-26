import { Component, OnInit, ViewChild } from '@angular/core';
import { EmployeeService } from '../Service/employee.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ModalComponent } from 'ng2-bs3-modal/ng2-bs3-modal';
import { Employee } from '../Model/employee';
import { DBOperation } from '../Shared/enum';

@Component({
    templateUrl: 'app/Components/employee.component.html'
})

export class EmployeeComponent implements OnInit {

    @ViewChild('modal') modal: ModalComponent;
    employees: Employee[];
    employee: Employee;
    msg: string;
    indLoading: boolean = false;
    employeeFrm: FormGroup;
    dbops: DBOperation;
    modalTitle: string;
    modalBtnTitle: string;

    constructor(private fb: FormBuilder, private _employeeService: EmployeeService) { }

    ngOnInit(): void {
        this.employeeFrm = this.fb.group({
            EmpId: [''],
            FirstName: ['', Validators.required],
            LastName: ['', Validators.required],
            EmailID: ['', Validators.required],
            Phone: ['', Validators.required]
        });
        this.LoadEmployees();
    }

    LoadEmployees(): void {
        this.indLoading = true;
        this._employeeService.get()
            .subscribe(data => { this.employees = data; this.indLoading = false; },
                error => this.msg = <any>error);
    }

    addEmployee() {
        this.dbops = DBOperation.create;
        this.SetControlsState(true);
        this.modalTitle = "Add New Employee";
        this.modalBtnTitle = "Add";
        this.employeeFrm.reset();
        this.modal.open();
    }

    editEmployee(empid: number) {
        this.dbops = DBOperation.update;
        this.SetControlsState(true);
        this.modalTitle = "Edit Employee";
        this.modalBtnTitle = "Update";
        this.employee = this.employees.filter(x => x.empId == empid)[0];
        this.employeeFrm.setValue({
            EmpId: this.employee.empId,
            FirstName: this.employee.firstName,
            LastName: this.employee.lastName,
            EmailID: this.employee.emailID,
            Phone: this.employee.phone
        });
        this.modal.open();
    }

    deleteEmployee(empid: number) {
        this.dbops = DBOperation.delete;
        this.SetControlsState(false);
        this.modalTitle = "Confirm to Delete?";
        this.modalBtnTitle = "Delete";
        this.employee = this.employees.filter(x => x.empId == empid)[0];
        this.employeeFrm.setValue({
            EmpId: this.employee.empId,
            FirstName: this.employee.firstName,
            LastName: this.employee.lastName,
            EmailID: this.employee.emailID,
            Phone: this.employee.phone
        });
        this.modal.open();
    }

    onSubmit(formData: any) {
        this.msg = "";

        switch (this.dbops) {
            case DBOperation.create:
                formData._value.EmpId = 0;                
                this._employeeService.post(formData._value).subscribe(
                    data => {
                        if (data) //Success
                        {
                            this.msg = "New employee successfully added.";
                            this.LoadEmployees();
                        }
                        else {
                            this.msg = "There is some issue in saving records, please contact to system administrator!"
                        }

                        this.modal.dismiss();
                    },
                    error => {
                        this.msg = error;
                    }
                );
                break;
            case DBOperation.update:
                this._employeeService.put(formData._value.EmpId, formData._value).subscribe(
                    data => {
                        if (data) //Success
                        {
                            this.msg = "Employee successfully updated.";
                            this.LoadEmployees();
                        }
                        else {
                            this.msg = "There is some issue in saving records, please contact to system administrator!"
                        }

                        this.modal.dismiss();
                    },
                    error => {
                        this.msg = error;
                    }
                );
                break;
            case DBOperation.delete:
                this._employeeService.delete(formData._value.EmpId).subscribe(
                    data => {
                        if (data) //Success
                        {
                            this.msg = "Employee successfully deleted.";
                            this.LoadEmployees();
                        }
                        else {
                            this.msg = "There is some issue in saving records, please contact to system administrator!"
                        }

                        this.modal.dismiss();
                    },
                    error => {
                        this.msg = error;
                    }
                );
                break;

        }
    }

    SetControlsState(isEnable: boolean) {
        isEnable ? this.employeeFrm.enable() : this.employeeFrm.disable();
    }   
}