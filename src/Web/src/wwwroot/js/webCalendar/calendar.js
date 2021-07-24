import ListView from "./views/listView.js";
import MonthView from "./views/monthView.js";
import WeekView from "./views/weekView.js";

// Enumeration of the calendar view types.
const View = {
  MONTH: new MonthView(),
  WEEK: new WeekView(),
  LIST: new ListView()
};

/**
 * Calendar.
 *
 * @class Calendar
 */
export default class Calendar {
  constructor() {
    // set default view
    this.currentView = new MonthView();
  }

  switch(viewType, response) {
    this.currentView = View[viewType];
    this.update(response);
  }

  update(response) {
    this.currentView.render(response);
  }
}