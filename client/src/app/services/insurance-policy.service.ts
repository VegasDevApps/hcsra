import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { InsurancePolicy } from '../models/insurance-policy';
import { NewUpdateInsurancePolicy } from '../models/new-update-insurance-policy';

@Injectable({
  providedIn: 'root'
})
export class InsurancePolicyService {

  private baseUrl = 'http://localhost:5145/api/policy';

  constructor(private http: HttpClient) { }

  getPoliciesByUserId(userId: number){
    return this.http.get<InsurancePolicy[]>(this.baseUrl + `/user/${userId}`);
  }

  deletePolicyById(policyId: number){
    return this.http.delete(this.baseUrl + `/${policyId}`);
  }

  getInsurancePolicyById(policyId: number){
    return this.http.get<InsurancePolicy>(this.baseUrl + `/${policyId}`);
  }

  updateInsurancePolicy(policy: NewUpdateInsurancePolicy){
    return this.http.put(this.baseUrl, policy);
  }

  addInsurancePolicy(policy: NewUpdateInsurancePolicy){
    return this.http.post(this.baseUrl, policy);
  }
}
