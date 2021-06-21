import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { NgxSpinnerService } from "ngx-spinner";
import { ToastrService } from 'ngx-toastr';
import { LoanDetail } from '../shared/models/lon-detail.model'
import { HttpService } from '../shared/services/http.service';

@Component({
  selector: 'app-loan-comparison',
  templateUrl: './loan-comparison.component.html',
  styleUrls: ['./loan-comparison.component.css']
})
export class LoanComparisonComponent implements OnInit {

  loanDetail = new LoanDetail();
  savingsAmount: number | null;
  constructor(private httpService: HttpService,
    private toasterService: ToastrService,
    private spinner: NgxSpinnerService) { }

  ngOnInit(): void {
    this.loanDetail.CustomerRate = 0;
    this.loanDetail.BorrowingAmount = 0;
  }

  submitForm(form: NgForm) {
    if (!form.valid || this.loanDetail.CustomerRate === 0 || this.loanDetail.BorrowingAmount === 0) return;
    this.savingsAmount = null;
    this.spinner.show();
    this.httpService.post('LoanComparison/GetSavingAmount', this.loanDetail)
      .subscribe(
        (amount: number) => {
          this.spinner.hide();
          this.savingsAmount = amount;
        },
        (error: any) => {
          this.spinner.hide();
          this.toasterService.error("An error occurred. Please try again");
        }
      );
  }
}
