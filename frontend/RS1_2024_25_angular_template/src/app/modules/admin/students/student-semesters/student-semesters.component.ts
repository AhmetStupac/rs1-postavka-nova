import { Component, OnInit } from '@angular/core';
import { StudentGetByIdEndpointService, StudentGetByIdResponse } from '../../../../endpoints/student-endpoints/student-get-by-id-endpoint.service';
import { ActivatedRoute, Router } from '@angular/router';
import { MyAuthService } from '../../../../services/auth-services/my-auth.service';
import { MatTableDataSource } from '@angular/material/table';
import { MyConfig } from '../../../../my-config';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-student-semesters',
  standalone: false,
  
  templateUrl: './student-semesters.component.html',
  styleUrl: './student-semesters.component.css'
})
export class StudentSemestersComponent implements OnInit{

   displayedColumns: string[] = ['id', 'akademskaGodina', 'godinaStudija', 'obnova' , 'datumUpisa' , 'snimio'];
   dataSource: MatTableDataSource<YOSGetResponse> = new MatTableDataSource<YOSGetResponse>();
   yos: YOSGetResponse[] = [];
   student: StudentGetByIdResponse | null = null;

    constructor(
      private studentService: StudentGetByIdEndpointService,
      private router: Router,
      private route: ActivatedRoute,
      private authService: MyAuthService,
      private http: HttpClient 
    ) {}

    ngOnInit()
    {
      this.loadData();
    }

    loadData()
    {
      this.route.params.subscribe( params=>{
        let id = params['id'];
        if(id)
        {
          this.studentService.handleAsync(id).subscribe(studentGet=>{
            this.student = studentGet
          })

          this.http.get<YOSGetResponse[]>(`${MyConfig.api_address}/yos/get/${id}`).subscribe(yos=>{
            this.yos = yos;
            this.dataSource.data = this.yos;
          })
        }
      })
    }
goToNewSemester()
{
  this.router.navigate(['/admin/students/semester/new', this.student!.id])
}

}



export interface YOSGetResponse
{
  id: number;
  studentId: number;
  snimio: string;
  akademskaGodinaId: number;
  akademskaGodina: string;
  godinaStudija: number;
  obnova: boolean;
  datumUpisa: string;
  cijenaSkolarine: number;
}
