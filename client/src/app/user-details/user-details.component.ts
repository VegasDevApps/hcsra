import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { of, switchMap, take } from 'rxjs';
import { UserService } from '../services/user.service';
import { User } from '../models/users';
import { Location } from '@angular/common';
import { InsurancePolicy } from '../models/insurance-policy';
import { InsurancePolicyService } from '../services/insurance-policy.service';
import { PolicyFormRouteData } from '../models/policy-form-route-data';

@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
  styleUrls: ['./user-details.component.scss']
})
export class UserDetailsComponent implements OnInit {

  user: User | null = null;
  insuracePolicies: InsurancePolicy[] = [];

  constructor(
    private userService: UserService,
    private policyService: InsurancePolicyService,
    private route: ActivatedRoute,
    private router: Router,
    private location: Location) { }


  ngOnInit(): void {
    this.loadDataforInit();
  }

  onDeleteClick(policyId: number) {
    this.policyService.deletePolicyById(policyId).pipe(switchMap(() => {
      return this.policyService.getPoliciesByUserId(this.user!.id).pipe(take(1));
    })).subscribe(newList => this.insuracePolicies = newList);
  }

  onNewClick() {
    
    const routeData: PolicyFormRouteData = {
      userId: this.user!.id,
      isEdit: false
    }
    this.navigateToPolicyForm(routeData);
    
  }

  onEditClick(policyId: number){
    const routeData: PolicyFormRouteData = {
      userId: this.user!.id,
      isEdit: true,
      editId: policyId
    }
    this.navigateToPolicyForm(routeData);
  }

  navigateToPolicyForm(data: PolicyFormRouteData){
    this.router.navigate(['/insurance-policy', data]);
  }

  loadDataforInit(){
    this.route.params.pipe(
      switchMap(params => {

        const userId = +params['userId'];
        
        if(isNaN(userId) || userId <= 0) {
          this.location.back();
          return of(null);
        }
        
        return this.userService.getUserBy(userId).pipe(take(1));
      }),
      switchMap(user => {
        
        if(!user) {
          this.location.back();
          return of(null);
        }

        this.user = user;
        return this.policyService.getPoliciesByUserId(user.id).pipe(take(1));
      })
    ).subscribe(policies => {
      
      if(!policies) {
        this.location.back();
      } else {
        this.insuracePolicies = policies;
      }
      
    });
  }
}
