import {Component, Inject} from '@angular/core';

@Component({
  selector: 'app-error-dialog',
  standalone: true,
  imports: [],
  templateUrl: './error-dialog.component.html',
  styleUrl: './error-dialog.component.scss'
})
export class ErrorDialogComponent {
  //constructor(@Inject(MAT_DIALOG_DATA) public data: {message: string}) {}
}
