import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { catchError } from 'rxjs';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const router = inject(Router)
  const toastr = inject(ToastrService)

  return next(req).pipe(
    catchError(error => {
      if(error) {
        switch(error.status){
          case 400:
            if(error.error.error){
              const modalStateErrors = [];
              for(const key in error.error.error){
                if(error.error.error[key]){
                  modalStateErrors.push(error.error.error[key])
                }
              }
              throw modalStateErrors.flat();
            }else{
              toastr.error(error.error, error.status)
            }
            break;
            case 401:
              toastr.error('Unauthorised', error.status)
              break;
              case 404:
                router.navigateByUrl('/not-found');
                break;
              case 500:
                const navigateExtras: NavigationExtras = {state: {error: error.error}}
                router.navigateByUrl('/server-error', navigateExtras);
                break;
            default:
              toastr.error('Something unexpected was wrong');
              break;
        }
      }
      throw error;
    })
  )
};
