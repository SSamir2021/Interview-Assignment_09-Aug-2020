"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var http_1 = require("@angular/http");
require("rxjs/add/operator/map");
require("rxjs/add/operator/do");
require("rxjs/add/operator/catch");
var EmployeeService = /** @class */ (function () {
    function EmployeeService(_http) {
        this._http = _http;
        this.API = 'http://localhost:55123/api';
        this.Employees_API = this.API + "/Employees";
    }
    EmployeeService.prototype.get = function () {
        return this._http.get(this.Employees_API)
            .map(function (response) { return response.json(); });
    };
    EmployeeService.prototype.post = function (employee) {
        return this._http.post(this.Employees_API, employee)
            .map(function (response) { return response.json(); });
    };
    EmployeeService.prototype.put = function (empid, employee) {
        return this._http.put(this.Employees_API + "/" + empid, employee)
            .map(function (response) { return response.json(); });
    };
    EmployeeService.prototype.delete = function (empid) {
        return this._http.delete(this.Employees_API + "/" + empid)
            .map(function (response) { return response.json(); });
    };
    EmployeeService = __decorate([
        core_1.Injectable(),
        __metadata("design:paramtypes", [http_1.Http])
    ], EmployeeService);
    return EmployeeService;
}());
exports.EmployeeService = EmployeeService;
//# sourceMappingURL=employee.service.js.map