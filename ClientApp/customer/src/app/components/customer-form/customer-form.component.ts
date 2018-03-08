import { CustomerService } from './../../services/customer.service';
import { ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Customer } from './../../../customer';
import { Component, OnInit, OnChanges, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { FormControl, FormBuilder, Validators } from '@angular/forms';
import { OnDestroy } from '@angular/core/src/metadata/lifecycle_hooks';
import { ISubscription } from 'rxjs/Subscription';
import * as lodash from 'lodash';
import { Router } from '@angular/router';

enum FormMode {EDIT, CREATE}

@Component({
  selector: 'app-customer-form',
  templateUrl: './customer-form.component.html',
  styleUrls: ['./customer-form.component.css']
})
export class CustomerFormComponent implements OnInit, OnDestroy {
  status: string;
  customer: Customer;
  form: FormGroup;
  sub: ISubscription;
  mode: FormMode;

  constructor(
    private fb: FormBuilder,
    private http: HttpClient,
    private route: ActivatedRoute,
    private router: Router,
    private service: CustomerService) {

    }

    ngOnInit() {
      this.initForm();
      const idParam = this.route.snapshot.paramMap.get('id');
      if (idParam === 'new') {
        this.mode = FormMode.CREATE;
      } else {
        this.mode = FormMode.EDIT;
        const id = +idParam;
        this.sub = this.service.getCustomerById(id).subscribe(
          resp => {
            console.log(resp);
            this.customer = {...resp};
            const data = lodash.omit(resp, 'id');
            this.form.setValue(data);
          }
        );
      }
    }

  ngOnDestroy(): void {
    if (this.sub) {
      this.sub.unsubscribe();
    }
  }

  onSave(event) {
    let action;
    if (this.mode === FormMode.CREATE) {
      const customerData = {...this.form.value};
      action = this.service.createCustomer(customerData);
    } else {
      const customerData = {...this.form.value, id: this.customer.id};
      action = this.service.updateCustomer(customerData);
    }

    this.status = 'Saving...';
    action.subscribe(
      customer => {
        this.status = 'Saved successfully.';
      },
      error => {
        this.status = error;
      }
    );
  }

  goBack() {
    this.router.navigate(['/customers']);
  }

  initForm() {
    this.form = this.fb.group({
      'name': ['', Validators.required],
      'surname': ['', Validators.required],
      'telephone': ['', Validators.required],
      'address': ['', Validators.required]
    });
  }

}
