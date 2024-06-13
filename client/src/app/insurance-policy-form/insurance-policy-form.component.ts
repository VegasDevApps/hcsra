import { Component, OnInit } from '@angular/core';
import { InsurancePolicyService } from '../services/insurance-policy.service';
import { ActivatedRoute, Router } from '@angular/router';
import { PolicyFormRouteData } from '../models/policy-form-route-data';
import { InsurancePolicy } from '../models/insurance-policy';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { take } from 'rxjs';
import { NewUpdateInsurancePolicy } from '../models/new-update-insurance-policy';

@Component({
  selector: 'app-insurance-policy-form',
  templateUrl: './insurance-policy-form.component.html',
  styleUrls: ['./insurance-policy-form.component.scss']
})
export class InsurancePolicyFormComponent implements OnInit {

  // arguments passed from details componet
  args?: PolicyFormRouteData;

  // original policy required for update
  policyForEdit?: InsurancePolicy;

  policyForm = new FormGroup({
    policyNumber: new FormControl('', [Validators.required, Validators.minLength(3)]),
    insuranceAmount: new FormControl('', [Validators.required, Validators.min(0)]),
    startDate: new FormControl('', [Validators.required]),
    endDate: new FormControl('', [Validators.required])
  });

  constructor(
    private policyService: InsurancePolicyService,
    private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit(): void {
    this.route.params.subscribe(data => {
      const routeData = data as PolicyFormRouteData;

      this.checkRouteData(routeData);

      // If is edit form, fetch policy
      if (this.args?.isEdit) {
        this.getPolicyForEdit();
      }

    });
  }

  
  submitForm() {

    if (this.args?.isEdit)
      this.submitChanges();
    else
      this.submitNew();

  }

  submitChanges() {
    const updatedPolicy = this.createNewUpdateInsurancePolicy();
    this.policyService.updateInsurancePolicy(updatedPolicy).subscribe(() => {
      this.navigateToDetails();
    });

  }

  submitNew() {
    const updatedPolicy = this.createNewUpdateInsurancePolicy();
    this.policyService.addInsurancePolicy(updatedPolicy).subscribe(() => {
      this.navigateToDetails();
    });
  }

  getPolicyForEdit() {
    if (this.args?.editId) {
      this.policyService.getInsurancePolicyById(this.args.editId)
        .pipe(take(1))
        .subscribe(policy => {
          if (policy) {
            this.policyForEdit = policy;
            this.populateForm(policy);
          }
        });
    }
  }

  populateForm(policyToPopulate: InsurancePolicy) {

    this.policyForm.setValue({
      policyNumber: policyToPopulate.policyNumber,
      insuranceAmount: policyToPopulate.insuranceAmount.toString(),
      startDate: policyToPopulate.startDate.toString(),
      endDate: policyToPopulate.endDate.toString()
    });

  }

  navigateToDetails() {
    const { userId } = this.args!;
    this.router.navigate(['/user-details', { userId }]);
  }

  createNewUpdateInsurancePolicy(): NewUpdateInsurancePolicy {
    const formValues = this.policyForm.value!;
    const { editId = 0, userId } = this.args!;

    const updatedPolicy: NewUpdateInsurancePolicy = {
      policyNumber: formValues.policyNumber!,
      insuranceAmount: +formValues.insuranceAmount!,
      startDate: formValues.startDate!,
      endDate: formValues.endDate!
    };

    if (this.args!.isEdit) {
      updatedPolicy.id = editId;
    } else {
      updatedPolicy.userId = userId;
    }

    return updatedPolicy;
  }

  // Insure that data is valid
  checkRouteData(data: PolicyFormRouteData): boolean {

    let provenData: PolicyFormRouteData = {
      userId: 0,
      isEdit: false
    };

    if (isNaN(data.userId)) {
      return false;
    } else {
      provenData.userId = +data.userId;
    }

    if (typeof data.isEdit === 'string') {
      provenData.isEdit = (data.isEdit === 'true');
    } else if (typeof data.isEdit === 'boolean') {
      provenData.isEdit = data.isEdit;
    } else {
      return false
    }

    if (!data.editId) { }
    else if (!isNaN(data.editId)) {
      provenData.editId = +data.editId;
    } else {
      return false;
    }
    this.args = provenData;
    return true;
  }
}


