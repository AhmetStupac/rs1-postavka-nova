import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MyConfig } from '../../my-config';
import { MyBaseEndpointAsync } from '../../helper/my-base-endpoint-async.interface';

@Injectable({
  providedIn: 'root'
})
export class StudentDeleteEndpointService implements MyBaseEndpointAsync<StudentDeleteRequest, void> {
  private apiUrl = `${MyConfig.api_address}/students`;

  constructor(private httpClient: HttpClient) {}

  handleAsync(req: StudentDeleteRequest) {
    return this.httpClient.delete<void>(this.apiUrl, {
      body: req
    });
  }
  
}

export interface StudentDeleteRequest
{
  id: number;
  deletedById?: number;
}