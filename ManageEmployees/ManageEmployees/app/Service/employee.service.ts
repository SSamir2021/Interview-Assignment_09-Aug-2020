import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/catch';
import { Employee } from '../Model/employee';

@Injectable()
export class EmployeeService {   
    public API = 'http://localhost:55123/api';
    public Employees_API = `${this.API}/Employees`;
    
    constructor(private _http: Http) {  }

    get(): Observable<Array<Employee>> {          
        return this._http.get(this.Employees_API)
            .map((response: Response) => <any>response.json());
    }

    post(employee: Employee): Observable<Employee> {        
        return this._http.post(this.Employees_API, employee)
            .map((response: Response) => <any>response.json());        
    }

    put(empid: number, employee: Employee): Observable<Employee> {        
        return this._http.put(`${this.Employees_API}/${empid}`, employee)
            .map((response: Response) => <any>response.json());
    }

    delete(empid: number) {        
        return this._http.delete(`${this.Employees_API}/${empid}`)
            .map((response: Response) => <any>response.json());
    }
}