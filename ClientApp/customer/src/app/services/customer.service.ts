import { Customer, Page } from './../../customer';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { HttpErrorResponse } from '@angular/common/http/src/response';
import { Observable } from 'rxjs/Observable';
import {catchError} from 'rxjs/operators';
import { ErrorObservable } from 'rxjs/observable/ErrorObservable';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type':  'application/json'
  })
};

@Injectable()
export class CustomerService {

  constructor(private http: HttpClient) { }

  getAllCustomers() {
    return this.http.get<Customer[]>('/api/customer/all');
  }

  getCustomerPage(pageIndex: number, pageSize: number) {
    return this.http.get<Page<Customer>>(`/api/customer/pages/${pageIndex}?pageSize=${pageSize}`);
  }

  getCustomerById(id: number) {
    return this.http.get<Customer>('/api/customer/' + id);
  }

  createCustomer(customer: Customer): Observable<Customer> {
    return this.http.post<Customer>('/api/customer', customer, httpOptions)
      .pipe(
        catchError(this.handleError)
      );
  }

  deleteCustomer(id: number): Observable<void> {
    return this.http.delete<void>('/api/customer/' + id, httpOptions)
      .pipe(
        catchError(this.handleError)
      );
  }

  updateCustomer(customer: Customer): Observable<Customer> {
    return this.http.put<Customer>('/api/customer/' + customer.id, customer, httpOptions)
      .pipe(
        catchError(this.handleError)
      );
  }

  private handleError(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
      console.error('An error occurred:', error.error.message);
    } else {
      console.error(
        `Backend returned code ${error.status}, ` +
        `body was: ${error.error}`);
    }
    return new ErrorObservable(
      'Operation failed; please try again later.');
  }

}
