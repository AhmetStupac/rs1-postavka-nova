import { Component, OnInit } from '@angular/core';
import { StudentGetByIdEndpointService, StudentGetByIdResponse } from '../../../../../endpoints/student-endpoints/student-get-by-id-endpoint.service';
import { ActivatedRoute, Router } from '@angular/router';
import { MyAuthService } from '../../../../../services/auth-services/my-auth.service';
import { HttpClient } from '@angular/common/http';
import { YOSGetResponse } from '../student-semesters.component';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MyConfig } from '../../../../../my-config';
import { MySnackbarHelperService } from '../../../../shared/snackbars/my-snackbar-helper.service';

@Component({
  selector: 'app-student-semesters-new',
  standalone: false,
  
  templateUrl: './student-semesters-new.component.html',
  styleUrl: './student-semesters-new.component.css'
})
export class StudentSemestersNewComponent implements OnInit{

     yos: YOSGetResponse[] = [];
     student: StudentGetByIdResponse | null = null;
     academicYear : AcademicYear[] = [];
     recordedById: number | null = null;

    constructor(
      private studentService: StudentGetByIdEndpointService,
      private router: Router,
      private route: ActivatedRoute,
      private authService: MyAuthService,
      private http: HttpClient,
      private snackbar: MySnackbarHelperService
    ) {}


    form = new FormGroup({
      datumUpisa: new FormControl('', Validators.required),
      akademskaGodinaId: new FormControl(0, Validators.required),
      godinaStudija: new FormControl(0, Validators.required),
      obnova: new FormControl(false),
      cijenaSkolarine: new FormControl(0, [Validators.required, Validators.min(50), Validators.max(2000)])
    })

    ngOnInit()
    {
      this.loadData();

      this.form.get('obnova')!.disable();
      this.form.get('cijenaSkolarine')!.disable();
       this.form.get('datumUpisa')!
       .setValue(new Date().toISOString().split('T')[0])

        this.form.get('godinaStudija')!.valueChanges.subscribe({
          next: (val) => {
            let isRenewing = this.yos.find(y=> y.godinaStudija === val) != undefined;
            this.form.get('obnova')!.setValue(isRenewing);
             this.form.get('cijenaSkolarine')!.setValue(isRenewing ? 400 : 1800);
          }
        })
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
          })

          this.http.get<AcademicYear[]>(`${MyConfig.api_address}/yos/academic-years`).subscribe(ay=>{
            this.academicYear = ay;
            this.form.get('akademskaGodinaId')!.setValue(this.academicYear[0].id)
          })

          
        }
      })


    }


    newSemester()
    {

      const req = {
        studentId : this.student?.id,
        snimioId: this.authService.getMyAuthInfo()?.userId,
        akademskaGodinaId: this.form.get('akademskaGodinaId')!.value,
        godinaStudija: this.form.get('godinaStudija')!.value,
        datumUpisa: this.form.get('datumUpisa')!.value,
      }


        this.http.post<number>(`${MyConfig.api_address}/yos/s/create`, req).subscribe({
          next: (val)=>{
            this.snackbar.showMessage("uspjesno");
          },
          error: (err)=>{
            this.snackbar.showMessage("AkademskaGodina vec postoji");
          }

        })
    }


}

export interface AcademicYear
{
  id: number;
  name: string;
}
