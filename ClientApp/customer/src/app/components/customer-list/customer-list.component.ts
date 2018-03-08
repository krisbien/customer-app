import { CustomerService } from './../../services/customer.service';
import { Customer } from './../../../customer';
import { Component, OnInit } from '@angular/core';
import {PageEvent} from '@angular/material';
import { DialogService } from '../../services/dialog.service';

@Component({
  selector: 'app-customer-list',
  templateUrl: './customer-list.component.html',
  styleUrls: ['./customer-list.component.css']
})
export class CustomerListComponent implements OnInit {
  isLoading = false;
  allCount = 0;
  pageIndex = 0;
  pageSize = 5;
  pageSizeOptions = [5, 10, 25, 100];
  customers: Customer[];
  status = '';

  constructor(
    private service: CustomerService,
    private dialog: DialogService
  ) { }

  onPage(event: PageEvent) {
    this.pageIndex = event.pageIndex;
    this.pageSize = event.pageSize;
    this.loadCustomers();
  }

  onDelete(customer: Customer) {
    const sub = this.dialog.confirm('Are you sure you want to delete this customer?').subscribe(
      val => {
        if (val) {
          this.deleteCustomer(customer.id);
        }
        sub.unsubscribe();
      }
    );
  }

  ngOnInit() {
    this.loadCustomers();
  }

  deleteCustomer(id: number) {
    this.service.deleteCustomer(id).subscribe(
      () => {
        this.loadCustomers();
      },
      error => {
        this.dialog.alert('Delete operation failed! Please try again.');
      }
    );
  }

  loadCustomers() {
    this.isLoading = true;
    this.status = '';
    this.service.getCustomerPage(this.pageIndex, this.pageSize).subscribe(
      data => {
        this.allCount = data.allCount;
        this.customers = [...data.items];
        this.isLoading = false;
      },
      error => {
        this.isLoading = false;
        this.status = 'Error: Could not load the data from server.';
        this.dialog.alert('Loading data failed! Please try refresh the page.');
      });
  }

}
