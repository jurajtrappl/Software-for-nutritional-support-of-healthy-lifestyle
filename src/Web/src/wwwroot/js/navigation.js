import Calendar from "./webCalendar/calendar.js";

// init
const calendar = new Calendar();

const updateMonthEndpoint = (year, month) =>
  `/App/Calendar/UpdateMonth?year=${year}&month=${month}`;

/**
 * AJAX call to change the month in the calendar.
 * @param {number} year
 * @param {number} month
 */
window.changeMonth = (year, month) => {
  $.ajax({
    type: "GET",
    url: updateMonthEndpoint(year, month),
    success: response => calendar.update(response),
    error: error => console.log(error)
  });
};

/**
 * AJAX call to change the view of the calendar to month.
 * @param {number} year
 * @param {number} month
 */
window.monthView = (year, month) => {
  $.ajax({
    type: "GET",
    url: updateMonthEndpoint(year, month),
    success: response => calendar.switch("MONTH", response),
    error: error => console.log(error)
  });
};

const updateWeekEndpoint = (year, month, startDay) =>
  `/App/Calendar/UpdateWeek?year=${year}&month=${month}&startDay=${startDay}`;

/**
 * AJAX call to change the week in calendar.
 * @param {number} year
 * @param {number} month
 * @param {number} startDay
 */
window.changeWeek = (year, month, startDay) => {
  $.ajax({
    type: "GET",
    url: updateWeekEndpoint(year, month, startDay),
    success: response => calendar.update(response),
    error: error => console.log(error)
  });
};

/**
 * AJAX call to change the view of the calendar to week.
 * @param {number} year
 * @param {number} month
 * @param {number} startDay
 */
window.weekView = (year, month, startDay) => {
  $.ajax({
    type: "GET",
    url: updateWeekEndpoint(year, month, startDay),
    success: response => calendar.switch("WEEK", response),
    error: error => console.log(error)
  });
};

// List stuff

const updateListEndpoint = (year, month, day) =>
  `/App/Calendar/UpdateList?year=${year}&month=${month}&day=${day}`;

/**
 * AJAX call to change the view of the calendar to list.
 * @param {number} year
 * @param {number} month
 * @param {number} day
 */
window.changeList = (year, month, day) => {
  $.ajax({
    type: "GET",
    url: updateListEndpoint(year, month, day),
    success: response => calendar.update(response),
    error: error => console.log(error)
  });
}

/**
 * AJAX call to change the view of the calendar to list.
 * @param {number} year
 * @param {number} month
 * @param {number} day
 */
window.listView = (year, month, day) => {
  $.ajax({
    type: "GET",
    url: updateListEndpoint(year, month, day),
    success: response => calendar.switch("LIST", response),
    error: error => console.log(error)
  });
}