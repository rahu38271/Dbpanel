import { Component, OnInit,ViewChild } from '@angular/core';
import { VoterdataService } from 'src/app/voterdata.service';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import {MatTableDataSource} from '@angular/material/table';


export interface Data {
  userId: string;
  Id: string;
  title: string;
  body: string;
}


@Component({
  selector: 'app-data',
  templateUrl: './data.component.html',
  styleUrls: ['./data.component.scss'],
})
export class DataComponent implements OnInit {
  displayedColumns: string[] = ['id', 'userId', 'title', 'body'];
  
  dataSource : any;

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  
  constructor(private voter:VoterdataService) { 
    this.dataSource = new MatTableDataSource();
    this.voter.getVoterData().subscribe(data=>{
      this.dataSource = data;
    })
  }

 
   applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  ngOnInit() {
       this.dataSource.paginator = this.paginator;
  }

}
