import CalendarView from "./calendarView.js";
import { toDatabaseKey, toDatabaseKeyDate, isSameDay, isSameHour, formatTime } from "../../date/utils.js";
import { showDrinkingRegime, showExercise, showMeal } from "../showPlanContent.js"

/**
 * WeekView.
 *
 * @class WeekView
 * @extends {CalendarView}
 */
export default class WeekView extends CalendarView {
  create(dates, weekDaysNames, mealPlanDates, exercisePlanDates) {
    //parent div element
    const view = $("<div>").attr("id", "viewTable");

    // weekDays md and bigger
    view.append(this.weekDays("calendarWeekDays", dates, weekDaysNames, false));

    // weekDays smaller than md
    view.append(this.weekDays("calendarWeekDaysMobile", dates, weekDaysNames, true));

    // week view
    const weekView = $("<div>").addClass("week");

    const timeColTimeData = $("<div>").addClass("timeCol");

    // first common col
    const commonCol = $("<div>").addClass("timeColHourField centered");
    timeColTimeData.append(commonCol);

    // next cols are times
    for (let i = 5; i < this.HOURS_IN_DAY - 2; i++) {
      const timeColumnTime = $("<div>")
        .addClass("timeColHourField centered")
        .text(`${i}:00`);
      timeColTimeData.append(timeColumnTime);
    }
    weekView.append(timeColTimeData);

    // map to js date objects
    mealPlanDates = mealPlanDates.map(d => new Date(d));
    const exercises = {};
    if (exercisePlanDates) {
      exercisePlanDates.forEach(({ key: date, value: duration }) => {
        exercises[new Date(date)] = duration;
      });
    }

    // the rest of the cols are week days
    for (let i = 0; i < this.WEEK_LENGTH; i++) {
      const currentDate = new Date(dates[i]);
      const fullDateTimeKey = toDatabaseKey(currentDate);
      const dateKey = toDatabaseKeyDate(currentDate);

      // hour cols
      const dayCol = $("<div>").addClass("dayCol");

      // common row col
      const commonHourField = $("<div>").addClass("hourField centered");

      const iconsDivCommon = $("<div>");

      // drinking regime icon
      if (mealPlanDates.some(d => isSameDay(d, currentDate))) {
        const icon = this.createIcon("DrinkingRegime", fullDateTimeKey)
          .click(() => showDrinkingRegime(dateKey));
        iconsDivCommon.append(icon);
      }

      // if any exercises, then find for this day
      if (exercises) {
        for (const [date, _amount] of Object.entries(exercises)) {
          const jsDate = new Date(date);
          if (isSameDay(jsDate, currentDate)) {
            const icon = this.createIcon("Exercise", fullDateTimeKey)
              .click(() => showExercise(dateKey));
            iconsDivCommon.append(icon);
            break;
          }
        }
      }
      commonHourField.append(iconsDivCommon);
      dayCol.append(commonHourField);

      // hour fields
      for (let hour = 5; hour < 22; hour++) {
        const time = `${hour}:00`;
        const hourField = $("<div>").addClass("hourField centered");

        currentDate.setHours(hour);

        // meal icon
        const iconsDiv = $("<div>");
        const mealDate = mealPlanDates.find(d => isSameHour(d, currentDate) && isSameDay(d, currentDate));
        if (mealDate !== undefined) {
          const icon = this.createIcon("Meal", toDatabaseKey(mealDate))
            .click(() => showMeal(dateKey, formatTime(mealDate)));
          iconsDiv.append(icon);
        }
        hourField.append(iconsDiv);

        dayCol.append(hourField);
      }
      weekView.append(dayCol);
    }

    return view.append(weekView);
  }

  render(response) {
    const { weekDaysNames, weekStartDate, weekEndDate, dates, mealPlanDates, exercisePlanDates } = response;

    // Heading update
    const weekStart = new Date(dates[0]);
    const weekEnd = new Date(dates[dates.length - 1]);
    $(".calendarHeading").each(function () {
      $(this).text(`${weekStartDate} - ${weekEndDate}`);
    });

    // Change content of the same view (pure JS for onclick)
    const previousWeekStart = new Date(weekStart.setDate(weekStart.getDate() - 7));
    const nextWeekStart = new Date(weekEnd.setDate(weekEnd.getDate() + 1));
    document.getElementById("btnPrev").onclick = () => changeWeek(previousWeekStart.getFullYear(), previousWeekStart.getMonth() + 1, previousWeekStart.getDate());
    document.getElementById("btnPrevMobile").onclick = () => changeWeek(previousWeekStart.getFullYear(), previousWeekStart.getMonth() + 1, previousWeekStart.getDate());

    const today = new Date();
    while (today.getDay() !== 0) {
      today.setDate(today.getDate() - 1);
    }
    document.getElementById("btnToday").onclick = () => changeWeek(today.getFullYear(), today.getMonth() + 1, today.getDate());
    document.getElementById("btnTodayMobile").onclick = () => changeWeek(today.getFullYear(), today.getMonth() + 1, today.getDate());

    document.getElementById("btnNext").onclick = () => changeWeek(nextWeekStart.getFullYear(), nextWeekStart.getMonth() + 1, nextWeekStart.getDate());
    document.getElementById("btnNextMobile").onclick = () => changeWeek(nextWeekStart.getFullYear(), nextWeekStart.getMonth() + 1, nextWeekStart.getDate());

    // Switching view handlers
    weekStart.setDate(weekStart.getDate() + 7);
    document.getElementById("btnMonth").onclick = () => monthView(weekStart.getFullYear(), weekStart.getMonth() + 1);
    document.getElementById("btnMonthMobile").onclick = () => monthView(weekStart.getFullYear(), weekStart.getMonth() + 1);
    document.getElementById("btnWeek").onclick = () => () => false;
    document.getElementById("btnWeekMobile").onclick = () => false;
    document.getElementById("btnList").onclick = () => listView(weekStart.getFullYear(), weekStart.getMonth() + 1, weekStart.getDate());
    document.getElementById("btnListMobile").onclick = () => listView(weekStart.getFullYear(), weekStart.getMonth() + 1, weekStart.getDate());

    // Content
    $("#viewTable").replaceWith(this.create(dates, weekDaysNames, mealPlanDates, exercisePlanDates));
  }

  weekDays(classList, dates, weekDayNames, shortened) {
    const parentDiv = $("<div>").addClass(classList);

    const timeColumn = $("<div>").addClass("calendarWeekDay");
    parentDiv.append(timeColumn);

    // week days itself - shifted correctly
    const firstWeekDay = new Date(dates[0]).getDay();
    let dayCounter = 0;
    while (dayCounter !== 7) {
      const weekDay = weekDayNames[(dayCounter + firstWeekDay) % 7];
      const weekDayDiv = $("<div>").addClass("calendarWeekDay");

      const jsDate = new Date(dates[dayCounter]);
      weekDayDiv.text(`${shortened ? weekDay.substr(0, 3) : weekDay} ${jsDate.getDate()}/${jsDate.getMonth() + 1}`);

      parentDiv.append(weekDayDiv);
      dayCounter++;
    }

    return parentDiv;
  }
}