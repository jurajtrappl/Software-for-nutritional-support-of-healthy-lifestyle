/**
 * Abstract class IconCreator.
 *
 * @class IconCreator
 */
class IconCreator {
  constructor(path, alt, cssClass) {
    if (this.constructor === IconCreator) {
      throw new Error("Abstract class can not be instantiated");
    }

    this.path = path;
    this.alt = alt;
    this.cssClass = cssClass;
  }

  /**
   * Default implementation of icon create method.
   * @param {string} dateKey
   */
  create(dateKey) {
    const anchor = $("<a>").addClass(this.cssClass).data("date", dateKey);

    const img = $("<img>")
      .attr("src", this.path)
      .attr("alt", this.alt);

    return anchor.append(img);
  }
}

/**
 * Provides logic for creating a drinking regime icon.
 *
 * @class DrinkingRegimeIconCreator
 */
class DrinkingRegimeIconCreator extends IconCreator {
  constructor() {
    super("../../../icons/water.png", "Scheduled drinking regime", "drinkingRegimeItem");
  }
}

/**
 * Provides logic for creating an exercise icon.
 *
 * @class ExerciseIconCreator
 */
class ExerciseIconCreator extends IconCreator {
  constructor() {
    super("../../../icons/exercise.png", "Scheduled exercise", "exerciseItem");
  }
}

/**
 * Provides logic for creating a meal icon.
 *
 * @class MealIconCreator
 */
class MealIconCreator extends IconCreator {
  constructor() {
    super("../../../icons/soup.png", "Scheduled meal", "mealItem");
  }
}

/**
 * Abstract class CalendarView.
 *
 * @class CalendarView
 */
export default class CalendarView {
  constructor() {
    if (this.constructor === CalendarView) {
      throw new Error("Abstract class can not be instantiated");
    }

    // data
    this.HOURS_IN_DAY = 24;

    this.WEEK_COUNT = 6;
    this.WEEK_LENGTH = 7;

    this.iconCreators = {
      "DrinkingRegime": new DrinkingRegimeIconCreator(),
      "Exercise": new ExerciseIconCreator(),
      "Meal": new MealIconCreator()
    }
  }

  create() {
    throw new Error("Method 'create()' must be implemented.");
  }

  render() {
    throw new Error("Method 'update()' must be implemented.");
  }

  createIcon(type, dateKey) {
    return this.iconCreators[type].create(dateKey);
  }
}