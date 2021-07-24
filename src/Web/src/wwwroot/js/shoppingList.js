const renderShoppingList = (shoppingList, colNames) => {
  // table
  const table = $("<table>").addClass("table table-bordered").attr("id", "currentShoppingList");

  // thead
  const tableHeader = $("<thead>").addClass("thead-light");

  // thead tr
  const tableHeaderRow = $("<tr>");
  for (const colName of colNames) {
    // thead tr th
    const col = $("<th>").attr("scope", "col").text(colName);
    tableHeaderRow.append(col);
  }
  tableHeader.append(tableHeaderRow);
  table.append(tableHeader);

  // tbody
  const tableBody = $("<tbody>");

  let count = 1;
  for (const [name, amount] of Object.entries(shoppingList)) {
    // tbody tr
    const ingredientRow = $("<tr>");

    // col #
    const number = $("<th>").attr("scope", "row").text(count++);
    ingredientRow.append(number);

    // col Name
    const ingredientName = $("<th>").text(name);
    ingredientRow.append(ingredientName);

    // col Amount
    const ingredientAmount = $("<th>").text(amount);
    ingredientRow.append(ingredientAmount);

    tableBody.append(ingredientRow);
  }
  table.append(tableBody);

  return table;
}

$('#shoppingListForm').submit(function (event) {
  event.preventDefault();

  if ($(this).valid()) {
    const token = $('#shoppingListRequestVerificationToken').val();

    $.ajax({
      type: 'POST',
      url: '/App/ShoppingList/GetShoppingList',
      contentType: "application/x-www-form-urlencoded",
      headers: { 'RequestVerificationToken': token },
      cache: false,
      data: $(this).serialize(),
      success: function ({ translatedShoppingList, colNames }) {
        // remove summary text if there is one
        $("#validationSummary").text("");
        $("#result").text("");

        const newShoppingList = renderShoppingList(translatedShoppingList, colNames);
        const parentDiv = $("<div>").attr("id", "list").addClass("shoppingListSummary");
        parentDiv.append(newShoppingList);
        $("#list").replaceWith(parentDiv);
      },
      error: function (xhr) {
        const { errorMessage } = $.parseJSON(xhr.responseText);
        $("#result").addClass("text-danger").text(errorMessage);
      }
    })
  }
});