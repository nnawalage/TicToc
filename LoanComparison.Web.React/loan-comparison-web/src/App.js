import "./App.css";
import React from "react";
import toast, { Toaster } from "react-hot-toast";
import { SpinnerComponent } from "react-element-spinner";
import LoanComparison from "./LoanComparison";

class App extends React.Component {
  constructor() {
    super();
    this.state = {
      showLoader: false,
    };
  }

  showHiderLoader = (show) => {
    this.setState({ showLoader: show });
  };

  showError = (message) => {
    toast.error(message);
  };

  render() {
    return (
      <div>
        <SpinnerComponent loading={this.state.showLoader} position="global" />
        <Toaster />
        <LoanComparison
          loaderHandler={this.showHiderLoader}
          showErrorHandler={this.showError}
        />
      </div>
    );
  }
}

export default App;
