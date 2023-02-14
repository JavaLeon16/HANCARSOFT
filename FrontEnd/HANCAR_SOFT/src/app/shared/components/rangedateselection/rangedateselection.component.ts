import { Component, Output, EventEmitter, Input, ViewChild, OnInit } from '@angular/core';
import { NgbDate, NgbCalendar, NgbDateParserFormatter } from '@ng-bootstrap/ng-bootstrap';
import { DateManager } from 'src/app/models/common/dateManager';

@Component({
  // tslint:disable-next-line: component-selector
  selector: 'rangedateselection',
  templateUrl: './rangedateselection.component.html',
  styleUrls: ['./rangedateselection.component.css']
})

export class RangedateselectionComponent implements OnInit {
  private dateManager: DateManager;
  hoveredDate: NgbDate | null = null;
  today = this.calendar.getToday();
  @ViewChild('datepicker') datepicker: any;
  private FROM_DATE: Date;
  @Input() firstTitle: string;
  @Input() secondTitle: string;
  @Input() showTitles: boolean;
  @Output() dateChange = new EventEmitter<void>();
  @Input() closeAfterSelect = true;
  @Input() readonly = false;
  @Input() leftContainerClass = '';
  @Input() rightContainerClass = '';
  @Input() placeholder = 'dd/mm/aaaa';

  @Output() fromDateChange = new EventEmitter<Date>();
  @Input() get fromDate(): Date | string {
    return this.FROM_DATE;
  }
  set fromDate(value: Date | string) {
    this.FROM_DATE = typeof value === 'string' ? this.dateManager.stringToDate(value) : value;
    this.NGB_FROM_DATE = this.dateManager.toNgbDate(value);
    this.fromDateChange.emit(this.FROM_DATE);
  }

  private TO_DATE: Date;
  @Output() toDateChange = new EventEmitter<Date>();
  @Input() get toDate(): Date | string {
    return this.TO_DATE;
  }
  set toDate(value: Date | string) {
    this.TO_DATE = typeof value === 'string' ? this.dateManager.stringToDate(value) : value;
    this.NGB_TO_DATE = this.dateManager.toNgbDate(value);
    this.toDateChange.emit(this.TO_DATE);
  }

  private STR_FROM_DATE: string;
  @Output() strFromDateChange = new EventEmitter<string>();
  @Input() get strFromDate(): string {
    return this.STR_FROM_DATE;
  }
  set strFromDate(value: string) {
    this.STR_FROM_DATE = value;
    this.NGB_FROM_DATE = this.dateManager.toNgbDate(value);
    this.strFromDateChange.emit(this.STR_FROM_DATE);
  }

  private STR_TO_DATE: string;
  @Output() strToDateChange = new EventEmitter<string>();
  @Input() get strToDate(): string {
    return this.STR_TO_DATE;
  }
  set strToDate(value: string) {
    this.STR_TO_DATE = value;
    this.NGB_TO_DATE = this.dateManager.toNgbDate(value);
    this.strToDateChange.emit(this.STR_TO_DATE);
  }

  private NGB_FROM_DATE: NgbDate | null;
  get ngbFromDate(): NgbDate | null {
    return this.NGB_FROM_DATE;
  }
  set ngbFromDate(value: NgbDate | null) {
    this.NGB_FROM_DATE = value;
    this.FROM_DATE = this.dateManager.ngbDateToDate(value);
    this.STR_FROM_DATE = this.dateManager.ngbDateToString(value);
    this.fromDateChange.emit(this.FROM_DATE);
    this.strFromDateChange.emit(this.STR_FROM_DATE);
    this.dateChange.emit();
  }

  private NGB_TO_DATE: NgbDate | null;
  get ngbToDate(): NgbDate | null {
    return this.NGB_TO_DATE;
  }
  set ngbToDate(value: NgbDate | null) {
    this.NGB_TO_DATE = value;
    this.TO_DATE = this.dateManager.ngbDateToDate(value);
    this.STR_TO_DATE = this.dateManager.ngbDateToString(value);
    this.toDateChange.emit(this.TO_DATE);
    this.strToDateChange.emit(this.STR_TO_DATE);
    this.dateChange.emit();
  }

  constructor(
    private calendar: NgbCalendar,
    public formatter: NgbDateParserFormatter
  ) {
    this.dateManager = new DateManager(formatter);
  }

  ngOnInit(): void {
    this.showTitles = typeof this.showTitles === 'undefined' && this.firstTitle && this.secondTitle ? true : this.showTitles;
  }

   onDateSelection(date: NgbDate): void {
    if (!this.ngbFromDate
      && !this.ngbToDate) {
      this.ngbFromDate = date;
    } else if (this.ngbFromDate && !this.ngbToDate && date && (date.after(this.ngbFromDate) || date.equals(this.ngbFromDate))) {
      this.ngbToDate = date;
      if (this.closeAfterSelect) {
        this.datepicker.close();
      }
    } else {
      this.ngbToDate = null;
      this.ngbFromDate = date;
    }
  }

  isHovered(date: NgbDate): boolean {
    return this.ngbFromDate && !this.ngbToDate && this.hoveredDate && date.after(this.ngbFromDate) && date.before(this.hoveredDate);
  }

  isInside(date: NgbDate): boolean {
    return this.ngbToDate && date.after(this.ngbFromDate) && date.before(this.ngbToDate);
  }

  isRange(date: NgbDate): boolean {
    return date.equals(this.ngbFromDate) || (this.ngbToDate && date.equals(this.ngbToDate)) || this.isInside(date) || this.isHovered(date);
  }

  validateInput(currentValue: NgbDate | null, input: string): NgbDate | null {
    const parsed = this.formatter.parse(input);
    return parsed && this.calendar.isValid(NgbDate.from(parsed)) ? NgbDate.from(parsed) : currentValue;
  }

  refresh(): void {
    this.ngbFromDate = this.today;
    this.ngbToDate = null;
    this.datepicker.close();
    setTimeout(() => {
      this.datepicker.open();
    }, 0);
  }
}
