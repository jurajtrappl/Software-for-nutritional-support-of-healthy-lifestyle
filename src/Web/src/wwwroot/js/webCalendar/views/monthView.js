import calendarView from "./calendarView.js";
import { toDatabaseKey, isSameDay } from "../../date/utils.js";
import { showDrinkingRegime, showExercise, showMeals } from "../showPlanContent.js"

/**
 * MonthView.
 *
 * @class MonthView
 * @extends {CalendarView}
 */
export default class MonthView extends calendarView {
  calculatePreviousMonthFrom(year, month) {
    let newYear = year;
    let newMonth;

    if (month === 1) {
      newYear--;
      newMonth = 12;
    } else {
      newMonth = month - 1;
    }

    return {
      newYear,
      newMonth
    };
  }

  calculateNextMonthFrom(year, month) {
    let newYear = year;
    let newMonth;

    if (month === 12) {
      newYear++;
      newMonth = 1;
    } else {
      newMonth = month + 1;
    }

    return {
      newYear,
      newMonth
    };
  }

  create(dates, weekDaysNames, mealPlanDates, exercisePlanDates) {
    // parent div element
    const view = $("<div>").attr("id", "viewTable");

    // weekDays
    view.append(this.weekDays("calendarWeekDays", weekDaysNames, false));

    //  weekDaysMobile
    view.append(this.weekDays("calendarWeekDaysMobile", weekDaysNames, true));

    // month table body div
    const tBody = $("<div>");

    // map to js date objects
    mealPlanDates = mealPlanDates.map(date => new Date(date));
    if (exercisePlanDates) {
      exercisePlanDates.map(date => new Date(date));
    }

    let dayCounter = 0;
    for (let week = 0; week < this.WEEK_COUNT; week++) {
      const weekRow = $("<div>").addClass("monthWeek");

      for (let weekDay = 0; weekDay < this.WEEK_LENGTH; weekDay++) {
        const date = new Date(dates[dayCounter]);
        const dateKey = toDatabaseKey(date);

        const day = $("<div>").addClass("monthDay position-relative");

        if (dates[dayCounter].isDisabled) {
          day.addClass("monthDayDisabled");
        }

        //day info
        const dayField = $("<div>")
          .addClass("monthDayField")
          .text(date.getDate());
        day.append(dayField);

        // icons
        const icons = $("<div>").addClass("position-absolute bottomLeft");

        if (mealPlanDates.some(d => isSameDay(d, date)) && !dates[dayCounter].isDisabled) {
          const mealsIcon = this.createIcon("Meal", dateKey).click(() => showMeals(dateKey));
          icons.append(mealsIcon);

          // if there is a plan, there must be a drinking regime
          const drinkingRegimeIcon = this.createIcon("DrinkingRegime", dateKey).click(() => showDrinkingRegime(dateKey));
          icons.append(drinkingRegimeIcon);
        }

        if (exercisePlanDates && exercisePlanDates.some(d => isSameDay(new Date(d), date)) && !dates[dayCounter].isDisabled) {
          const exerciseIcon = this.createIcon("Exercise", dateKey).click(() => showExercise(dateKey));
          icons.append(exerciseIcon);
        }
        day.append(icons);
        weekRow.append(day);

        dayCounter++;
      }
      tBody.append(weekRow);
    }
    view.append(tBody);

    return view;
  }

  render(response) {
    const { monthsNames, weekDaysNames, monthNum, year, dates, mealPlanDates, exercisePlanDates } = response;

    // Heading update
    $(".calendarHeading").each(function () {
      $(this).text(`${monthsNames[monthNum - 1]} ${year}`);
    })

    // Change content of the same view (pure JS for onclick)
    const { newYear: yearForPrevious, newMonth: monthNumForPrevious } = this.calculatePreviousMonthFrom(year, monthNum);
    const { newYear: yearForNext, newMonth: monthNumForNext } = this.calculateNextMonthFrom(year, monthNum);

    document.getElementById("btnPrev").onclick = () => changeMonth(yearForPrevious, monthNumForPrevious);
    document.getElementById("btnPrevMobile").onclick = () => changeMonth(yearForPrevious, monthNumForPrevious);

    const today = new Date();
    document.getElementById("btnToday").onclick = () => monthView(today.getFullYear(), today.getMonth() + 1);
    document.getElementById("btnTodayMobile").onclick = () => monthView(today.getFullYear(), today.getMonth() + 1);

    document.getElementById("btnNext").onclick = () => changeMonth(yearForNext, monthNumForNext);
    document.getElementById("btnNextMobile").onclick = () => changeMonth(yearForNext, monthNumForNext);

    // Switching view handlers
    document.getElementById("btnMonth").onclick = () => false;
    document.getElementById("btnMonthMobile").onclick = () => false;

    const firstDay = new Date(dates[0]);
    document.getElementById("btnWeek").onclick = () => weekView(firstDay.getFullYear(), firstDay.getMonth() + 1, firstDay.getDate());
    document.getElementById("btnWeekMobile").onclick = () => weekView(firstDay.getFullYear(), firstDay.getMonth() + 1, firstDay.getDate());

    document.getElementById("btnList").onclick = () => listView(firstDay.getFullYear(), firstDay.getMonth() + 1, firstDay.getDate());
    document.getElementById("btnListMobile").onclick = () => listView(firstDay.getFullYear(), firstDay.getMonth() + 1, firstDay.getDate());

    // Content
    $("#viewTable").replaceWith(this.create(dates, weekDaysNames, mealPlanDates, exercisePlanDates));
  }

  weekDays(classList, names, shortened) {
    const weekDays = $("<div>").addClass(classList);
    for (const weekDayName of names) {
      const weekDayDiv = $("<div>")
        .addClass("calendarWeekDay")
        .text(shortened ? weekDayName.substr(0, 3) : weekDayName);
      weekDays.append(weekDayDiv);
    }
    return weekDays;
  }
}