const ingredientsTable = ingredients => {
  const tbody = $("<tbody>").attr("id", "Ingredients");
  for (const [name, amount] of Object.entries(ingredients)) {
    const ingredientName = $("<td>").text(name);
    const ingredientAmount = $("<td>").text(amount);

    const row = $("<tr>").append(ingredientName).append(ingredientAmount);

    tbody.append(row);
  }
  return tbody;
}

const macroNutrientsTable = (macroNutrients, translatedMacroNutrients) => {
  const tbody = $("<tbody>").attr("id", "NutritionAmounts");
  for (const [name, amount] of Object.entries(macroNutrients)) {
    const macroNutrientName = $("<td>").text(translatedMacroNutrients[name]);
    const macroNutrientAmount = $("<td>").text(amount);

    const row = $("<tr>").append(macroNutrientName).append(macroNutrientAmount);

    tbody.append(row);
  }
  return tbody;
}

window.showMealOnHomePage = date => {
  $.ajax({
    type: "GET",
    url: `/App/Home/ShowMeal?date=${date}`,
    success: function ({ scheduledMeal, translatedMacroNutrients }) {
      const { type, ingredients, macroNutrients } = scheduledMeal;
      $("#Ingredients").replaceWith(ingredientsTable(ingredients));
      $("#NutritionAmounts").replaceWith(macroNutrientsTable(macroNutrients, translatedMacroNutrients));
    },
    error: function (error, _type) {
      console.log(error);
    }
  });
}

// horizontal slider container
function updateSliderArrowsStatus(
  cardsContainer,
  containerWidth,
  cardCount,
  cardWidth
) {
  if ($(cardsContainer).scrollLeft() + containerWidth < cardCount * cardWidth + 15) {
    $("#slide-right-container").addClass("active");
  } else {
    $("#slide-right-container").removeClass("active");
  }
  if ($(cardsContainer).scrollLeft() > 0) {
    $("#slide-left-container").addClass("active");
  } else {
    $("#slide-left-container").removeClass("active");
  }
}

$(function () {
  // Scroll products' slider left/right
  const div = $("#cards-container");
  const cardCount = $(div)
    .find(".cards")
    .children(".planCard").length;
  const speed = 1000;
  let containerWidth = $(".planContainer").width();
  const cardWidth = 250;

  updateSliderArrowsStatus(div, containerWidth, cardCount, cardWidth);

  //Remove scrollbars
  $("#slide-right-container").click(function (e) {
    if ($(div).scrollLeft() + containerWidth < cardCount * cardWidth) {
      $(div).animate(
        {
          scrollLeft: $(div).scrollLeft() + cardWidth
        },
        {
          duration: speed,
          complete: function () {
            setTimeout(
              updateSliderArrowsStatus(
                div,
                containerWidth,
                cardCount,
                cardWidth
              ),
              1005
            );
          }
        }
      );
    }
    updateSliderArrowsStatus(div, containerWidth, cardCount, cardWidth);
  });
  $("#slide-left-container").click(function (e) {
    if ($(div).scrollLeft() + containerWidth > containerWidth) {
      $(div).animate(
        {
          scrollLeft: "-=" + cardWidth
        },
        {
          duration: speed,
          complete: function () {
            setTimeout(
              updateSliderArrowsStatus(
                div,
                containerWidth,
                cardCount,
                cardWidth
              ),
              1005
            );
          }
        }
      );
    }
    updateSliderArrowsStatus(div, containerWidth, cardCount, cardWidth);
  });

  // If resize action ocurred then update the container width value
  $(window).resize(function () {
    try {
      containerWidth = $("#cards-container").width();
      updateSliderArrowsStatus(div, containerWidth, cardCount, cardWidth);
    } catch (error) {
      console.log(
        `Error occured while trying to get updated slider container width:
            ${error}`
      );
    }
  });
});