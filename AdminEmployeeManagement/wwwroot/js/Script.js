$(document).ready(function () {
	$("#addMoreBtn").click(function () {
		$(".MultipleRecord .duplicate-row:first-child").clone().prependTo(".MultipleRecord").find("input[type='text'], input[type='email'], input[type='checkbox'], select").val("").prop("checked", false).end();
	});

	$(document).on('click', '.btn-remove', function () {
		if ($(".MultipleRecord .duplicate-row").length > 1) {
			$(this).parents(".duplicate-row").remove();
		}
	});

	$("#submitBtn").click(function (e) {
		e.preventDefault();
		var employeeData = [];

		$(".MultipleRecord .duplicate-row").each(function () {
			var employee = {
				Name: $(this).find("[name='Name']").val(),
				Email: $(this).find("[name='Email']").val(),
				Mobile: $(this).find("[name='Mobile']").val(),
				Department_Id: $(this).find("[name='Department_Id']").val(),
				Status: $(this).find("[name='Status']").prop('checked')
			};

			employeeData.push(employee);
		});

		$.ajax({
			type: "POST",
			url: "/InsertMultipleEmployees/InsertMultipleEmployees",
			data: JSON.stringify(employeeData),
			contentType: "application/json; charset=utf-8",
			dataType: "json",
			success: function (response) {
				if (response.success == true) {
					console.log("Success:", response);
					alert("Success!");
				} else {
					console.log("Insertion failed:", response);
					alert("Insertion failed.");
				}
				window.location.href = "/Home";
			},
			error: function (xhr, textStatus, errorThrown) {
				console.log("Error:", xhr, textStatus, errorThrown);
				alert("Error!");
			}
		});
	});
});