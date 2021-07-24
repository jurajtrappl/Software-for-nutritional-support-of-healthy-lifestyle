$('#bmiCalcForm').submit(function (event) {
  event.preventDefault();

  if ($(this).valid()) {
    const token = $('#suitablePlanCalcRequestVerificationToken').val();

    $.ajax({
      type: 'POST',
      url: "/Main/Home/FindSuitablePlans",
      contentType: "application/x-www-form-urlencoded",
      headers: { 'RequestVerificationToken': token },
      cache: false,
      data: $(this).serialize(),
      success: function ({ suitablePlansHeading, suitablePlans, bmi }) {
        // delete possible validation summary
        $(".validation-summary-errors").remove();

        const result = $("<div>").attr("id", "result").addClass("text-center paddingDelimiter15");

        const bmiHeading = $("<h1>").text("BMI");
        result.append(bmiHeading);

        const bmiValue = $("<p>").addClass("fontLarger fontLight").text(bmi);
        result.append(bmiValue);

        const heading = $("<h1>").text(suitablePlansHeading);
        result.append(heading);

        const mssg = suitablePlans.length == 0 ? "-" : suitablePlans.join(",");
        const plans = $("<p>").addClass("fontLarger fontLight").text(mssg);
        result.append(plans);

        $("#result").replaceWith(result);
      },
      error: function (error) {
        console.log(error);
      }
    });
  }
  else {
    $("#result").text("");
  }
});