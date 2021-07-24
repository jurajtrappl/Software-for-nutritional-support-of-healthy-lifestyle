import calendarView from "./calendarView.js"
import { toDatabaseKey, toDatabaseKeyDate } from "../../date/utils.js";
import { showDrinkingRegime, showExercise, showMeal } from "../showPlanContent.js";

export default class ListView extends calendarView {
  create(listViewDates, formattedDates, planItems) {
    // parent div element
    const view = $("<div>").attr("id", "viewTable");

    // body div
    const listView = $("<div>").addClass("list");

    let counter = 0;
    for (const date of listViewDates) {
      const currentDate = new Date(date);
      const fullDateTimeKey = toDatabaseKey(currentDate);
      const dateKey = toDatabaseKeyDate(currentDate);

      // header for date
      const header = $("<div>")
        .addClass("listItem text-center dateHeader")
        .text(formattedDates[counter]);
      listView.append(header);

      // row for icons
      const iconsRow = $("<div>")
        .addClass("listItem text-center centered iconRowHeight");

      const { drinkingRegime, exercise, meals } = planItems[counter];

      if (drinkingRegime) {
        const icon = this.createIcon("DrinkingRegime", dateKey)
          .addClass("iconListViewMargin")
          .click(() => showDrinkingRegime(dateKey));
        iconsRow.append(icon);
      }

      if (exercise) {
        const icon = this.createIcon("Exercise", fullDateTimeKey)
          .addClass("iconListViewMargin")
          .click(() => showExercise(dateKey));
        iconsRow.append(icon);
      }

      if (meals) {
        for (const mealDate of Object.keys(meals)) {
          const jsDateOfMeal = new Date(mealDate);
          const icon = this.createIcon("Meal", toDatabaseKey(jsDateOfMeal))
            .addClass("iconListViewMargin")
            .click(() => showMeal(dateKey, `${jsDateOfMeal.getHours()}:${jsDateOfMeal.getMinutes()}`));
          iconsRow.append(icon);
        }
      }

      listView.append(iconsRow);
      counter++;
    }

    return view.append(listView);
  }

  getPrevDayFrom(day, month, year) {
    const date = new Date(year, month, day);
    date.setDate(date.getDate() - 9);
    return date;
  }

  getNextDayFrom(day, month, year) {
    const date = new Date(year, month, day);
    date.setDate(date.getDate() + 1);
    return date;
  }

  render(response) {
    const { dates, cultureFormattedDates, planItems } = response;

    // Heading update
    $(".calendarHeading").each(function () {
      $(this).text(`${cultureFormattedDates[0]} - ${cultureFormattedDates[8]}`);
    });

    // Change content of the same view (pure JS for onclick)
    const firstDay = new Date(dates[0]);
    const lastDay = new Date(dates[8]);
    const prevDay = this.getPrevDayFrom(firstDay.getDate(), firstDay.getMonth(), firstDay.getFullYear());
    const nextDay = this.getNextDayFrom(lastDay.getDate(), lastDay.getMonth(), lastDay.getFullYear());

    document.getElementById("btnPrev").onclick = () => changeList(prevDay.getFullYear(), prevDay.getMonth() + 1, prevDay.getDate());
    document.getElementById("btnPrevMobile").onclick = () => changeList(prevDay.getFullYear(), prevDay.getMonth() + 1, prevDay.getDate());
    const today = new Date();
    document.getElementById("btnToday").onclick = () => changeList(today.getFullYear(), today.getMonth() + 1, today.getDate());
    document.getElementById("btnTodayMobile").onclick = () => changeList(today.getFullYear(), today.getMonth() + 1, today.getDate());
    document.getElementById("btnNext").onclick = () => changeList(nextDay.getFullYear(), nextDay.getMonth() + 1, nextDay.getDate());
    document.getElementById("btnNextMobile").onclick = () => changeList(nextDay.getFullYear(), nextDay.getMonth() + 1, nextDay.getDate());

    // Switching view handlers
    document.getElementById("btnMonth").onclick = () => monthView(firstDay.getFullYear(), firstDay.getMonth() + 1);
    document.getElementById("btnMonthMobile").onclick = () => monthView(firstDay.getFullYear(), firstDay.getMonth() + 1);
    document.getElementById("btnWeek").onclick = () => weekView(firstDay.getFullYear(), firstDay.getMonth() + 1, firstDay.getDate());
    document.getElementById("btnWeekMobile").onclick = () => weekView(firstDay.getFullYear(), firstDay.getMonth() + 1, firstDay.getDate());
    document.getElementById("btnList").onclick = () => false;
    document.getElementById("btnListMobile").onclick = () => false;

    // Content
    $("#viewTable").replaceWith(this.create(dates, cultureFormattedDates, planItems));
  }
}