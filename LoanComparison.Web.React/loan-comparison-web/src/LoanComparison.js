import React from "react";

class LoanComparison extends React.Component {
  constructor() {
    super();
    this.state = {
      loanDetail: {
        borrowingAmount: 0,
        customerRate: 0,
      },
      borrowinAmountErrorMessage: null,
      customerRateErrorMessage: null,
      savingsAmountElement: null,
    };
  }

  borrowingAmountChangeHandler = (event) => {
    this.setState({ borrowinAmountErrorMessage: null });
    const updateLoanDetail = this.state.loanDetail;
    updateLoanDetail.borrowingAmount = event.target.value;
    this.setState({ loanDetail: updateLoanDetail });
    this.validBorrowingAmount();
  };

  customerRateChangeHandler = (event) => {
    this.setState({ customerRateErrorMessage: null });
    const updateLoanDetail = this.state.loanDetail;
    updateLoanDetail.customerRate = event.target.value;
    this.setState({ loanDetail: updateLoanDetail });
    this.validCustomerRate();
  };

  submitHandler = (event) => {
    event.preventDefault();
    this.setState({ borrowinAmountErrorMessage: null });
    this.setState({ customerRateErrorMessage: null });
    const validBorrowingAmount = this.validBorrowingAmount();
    const validCustomerRate = this.validCustomerRate();
    if (validBorrowingAmount && validCustomerRate) {
      this.getSavingsAmount();
    }
  };

  validBorrowingAmount() {
    if (this.state.loanDetail.borrowingAmount <= 0) {
      this.setState({
        borrowinAmountErrorMessage: (
          <span className="text-danger ps-0">
            Please enter valid Borrowin Amount
          </span>
        ),
      });
      return false;
    }
    return true;
  }

  validCustomerRate() {
    if (this.state.loanDetail.customerRate <= 0) {
      this.setState({
        customerRateErrorMessage: (
          <span className="text-danger ps-0">
            Please enter valid Current Rate
          </span>
        ),
      });
      return;
    }
    return true;
  }

  getSavingsAmount() {
    this.props.loaderHandler(true);
    const requestOptions = {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(this.state.loanDetail),
    };
    fetch(
      `${process.env.REACT_APP_API_BASEURL}LoanComparison/GetSavingAmount`,
      requestOptions
    )
      .then((response) => {
        return response.json();
      })
      .then(
        (result) => {
          this.setState({
            savingsAmountElement: (
              <div className="row">
                <label htmlFor="savingsAmount" className="ps-0 font-bold">
                  Savings
                </label>
                <h4 className="saving-amount ps-0">
                  $
                  {result.toLocaleString(undefined, {
                    maximumFractionDigits: 2,
                  })}
                </h4>
              </div>
            ),
          });

          this.props.loaderHandler(false);
        },
        (error) => {
          this.props.loaderHandler(false);
          this.props.showErrorHandler(
            "An error has occurred. Please try again"
          );
        }
      );
  }

  render() {
    return (
      <div>
        <form onSubmit={this.submitHandler}>
          <div className="row">
            <div className="col-lg-4  col-2"></div>
            <div className="col-lg-4  col-8">
              <div className="row p-0 mt-2 mb-3">
                <h4>Find Savings Amount</h4>
              </div>
              <div className="row mb-4">
                <label
                  htmlFor="borrowingAmount"
                  className="ps-0 mb-2 font-bold">
                  Borrowing Amount
                </label>
                <input
                  type="range"
                  className="form-control-range mb-2"
                  name="borrowingRange"
                  min="0"
                  max="1000000"
                  value={this.state.loanDetail.borrowingAmount}
                  onChange={this.borrowingAmountChangeHandler}
                />
                <div className="input-group mb-2 p-0">
                  <div className="input-group-prepend">
                    <div className="input-group-text">$</div>
                  </div>
                  <input
                    type="number"
                    className="form-control mb-2"
                    name="borrowingAmount"
                    min="0"
                    max="1000000"
                    step="any"
                    placeholder="Enter Borrowing Amount"
                    required
                    value={this.state.loanDetail.borrowingAmount}
                    onChange={this.borrowingAmountChangeHandler}
                  />
                </div>
                {this.state.borrowinAmountErrorMessage}
              </div>
              <div className="row mb-4">
                <label htmlFor="currentRate" className="ps-0 mb-2 font-bold">
                  Current Rate
                </label>
                <input
                  type="range"
                  className="form-control-range mb-2"
                  name="rateRange"
                  min="0"
                  max="100"
                  value={this.state.loanDetail.customerRate}
                  onChange={this.customerRateChangeHandler}
                />
                <input
                  type="number"
                  className="form-control mb-2"
                  name="currentRate"
                  min="0"
                  max="100"
                  step="any"
                  placeholder="Enter Current Rate"
                  required
                  value={this.state.loanDetail.customerRate}
                  onChange={this.customerRateChangeHandler}
                />
                {this.state.customerRateErrorMessage}
              </div>
              <div className="row mb-4">
                <div className="col-8 p-0">
                  <button
                    type="submit"
                    className="btn btn-primary btn-lg rounded-pill">
                    <span className="btn-label">
                      Submit <i className="fas fa-long-arrow-alt-right"></i>
                    </span>
                  </button>
                </div>
              </div>
              {this.state.savingsAmountElement}
            </div>
          </div>
        </form>
      </div>
    );
  }
}

export default LoanComparison;
