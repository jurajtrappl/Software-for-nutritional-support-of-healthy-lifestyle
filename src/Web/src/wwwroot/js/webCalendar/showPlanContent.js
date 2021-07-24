// List of id's of the html elements where results will be displayed
const modalLabelId = "dayContentModalLabel";
const modalContentId = "dayContentModalBodyContent";
const modalId = "dayContentModal";

/**
 * Produces a table with ingredients and their amounts.
 * @param {Object} ingredients
 * @param {Array<string>} colNames
 */
const createIngredientsTable = (ingredients, colNames) => {
  // ingredients and macro nutrients summary table
  const ingredientsTable = $("<table>").addClass("table");

  // creating thead
  const ingredientsTableHead = $("<thead>");
  const ingredientsTableHeadRow = $("<tr>");
  colNames.map(name => ingredientsTableHeadRow.append($("<th>").text(name)));
  ingredientsTableHead.append(ingredientsTableHeadRow);
  ingredientsTable.append(ingredientsTableHead);

  // creating tbody
  const tableBody = $("<tbody>");

  // summary table
  Object.entries(ingredients).map(([name, amount]) => {
    const row = $("<tr>");

    const nameData = $("<td>").text(name);
    row.append(nameData);

    const amountData = $("<td>").text(amount);
    row.append(amountData);

    tableBody.append(row);
  });
  ingredientsTable.append(tableBody);

  return ingredientsTable;
};

/**
 * Produces a list of tables of ingredients and their amount for the given meals.
 * @param {Object} meals
 * @param {Array<string>} colNames
 */
const createMeals = (mealsData, colNames) => {
  const content = $("<div>")
    .attr("id", modalContentId)
    .addClass("mealsSummary");

  for (const [nameTime, meal] of Object.entries(mealsData)) {
    const mealTabPane = $("<div>");

    const nameTimeParagraph = $("<h4>")
      .addClass("text-center")
      .text(nameTime);
    mealTabPane.append(nameTimeParagraph);

    const { ingredients, ...rest } = meal;
    mealTabPane.append(createIngredientsTable(ingredients, colNames));

    content.append(mealTabPane);
  }

  return content;
};

/**
 * Produces a summary about an exercise.
 * @param {number} duration
 * @param {string} type
 */
const createExercise = (duration, type) => {
  const content = $("<div>").attr("id", modalContentId);

  const description = $("<h4>")
    .addClass("text-center")
    .text(`${type}: ${duration} min`);
  content.append(description);

  return content;
};

/**
 * Produces a summary about a drinking regime.
 * @param {Object} destructured IScheduledDrink
 */
const createDrinkingRegime = (amount) => {
  const content = $("<div>").attr("id", modalContentId);

  const description = $("<h4>")
    .addClass("text-center")
    .text(`${amount} l.`);
  content.append(description);

  return content;
};

const showDrinkingRegime = date => {
  $.ajax({
    type: "GET",
    url: `/App/Calendar/ShowDrinkingRegime?date=${date}`,
    success: ({ amount, cultureFormattedDate }) => {
      // header
      $("#" + modalLabelId).text(cultureFormattedDate);

      // content
      $("#" + modalContentId).replaceWith(createDrinkingRegime(amount));

      // trigger
      $("#" + modalId).modal({ show: true });
    },
    error: error => console.log(error)
  });
}

window.showDrinkingRegime = showDrinkingRegime;

const showExercise = date => {
  $.ajax({
    type: "GET",
    url: `/App/Calendar/ShowExercise?date=${date}`,
    success: ({ duration, type, cultureFormattedDate }) => {
      // header
      $("#" + modalLabelId).text(cultureFormattedDate);

      // content
      $("#" + modalContentId).replaceWith(createExercise(duration, type));

      // trigger
      $("#" + modalId).modal({ show: true });
    },
    error: error => console.log(error)
  });
}

window.showExercise = showExercise;

const showMeals = date => {
  $.ajax({
    type: "GET",
    url: `/App/Calendar/ShowMeals?date=${date}`,
    success: ({ mealsData, colNames, cultureFormattedDate }) => {
      // header
      $("#" + modalLabelId).text(cultureFormattedDate);

      // content
      $("#" + modalContentId).replaceWith(createMeals(mealsData, colNames));

      // trigger
      $("#" + modalId).modal({ show: true });
    },
    error: error => console.log(error)
  });
}

window.showMeals = showMeals;

const showMeal = (date, time) => {
  $.ajax({
    type: "GET",
    url: `/App/Calendar/ShowMeal?date=${date}&time=${time}`,
    success: ({ mealsData, colNames, cultureFormattedDate }) => {
      // header
      $("#" + modalLabelId).text(cultureFormattedDate);

      // content
      $("#" + modalContentId).replaceWith(createMeals(mealsData, colNames));

      // trigger
      $("#" + modalId).modal({ show: true });
    },
    error: error => console.log(error)
  });
}

window.showMeals = showMeals;

export {
  showDrinkingRegime,
  showExercise,
  showMeals,
  showMeal
};