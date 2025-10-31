import { useState, type ChangeEvent, type FormEvent } from "react";
import { LogInput } from "./LogInput";
import styles from "./LogForm.module.scss";

interface FormData {
	date: string;
	startTime: string;
	endTime: string;
	numOrders: string;
	earnings: string;
	bonus: string;
	distance: string;
}

interface FormErrors {
	date?: string;
	startTime?: string;
	endTime?: string;
	numOrders?: string;
	earnings?: string;
	bonus?: string;
	distance?: string;
}

export const LogForm = () => {
	const today = new Date().toISOString().split("T")[0];

	const now = new Date();
	const hh = String(now.getHours()).padStart(2, "0");
	const mm = String(now.getMinutes()).padStart(2, "0");

	const [formData, setFormData] = useState<FormData>({
		date: today,
		startTime: `${hh}:${mm}`,
		endTime: `${hh}:${mm}`,
		numOrders: "",
		earnings: "",
		bonus: "",
		distance: "",
	});

	const [errors, setErrors] = useState<FormErrors>({});
	const [submitted, setSubmitted] = useState(false);

	const handleInputChange = (e: ChangeEvent<HTMLInputElement>) => {
		const { name, value } = e.target;

		setFormData((prev) => ({
			...prev,
			[name]: value,
		}));

		if (errors[name as keyof FormErrors]) {
			setErrors((prev) => ({
				...prev,
				[name]: undefined,
			}));
		}
	};

	const validateForm = (): boolean => {
		const newErrors: FormErrors = {};

		if (!formData.date) {
			newErrors.date = "Date is required.";
		} else {
			const year = new Date(formData.date).getFullYear();
			if (year < 2025) newErrors.date = "Year cannot be earlier than 2025.";
		}

		if (!formData.startTime) newErrors.startTime = "Start time is required.";
		if (!formData.endTime) newErrors.endTime = "End time is required.";

		if (formData.startTime && formData.endTime) {
			const start = new Date(`${formData.date}T${formData.startTime}:00`);
			const end = new Date(`${formData.date}T${formData.endTime}:00`);

			console.log(start);
			console.log(end);
			if (end < start) {
				newErrors.endTime = "End time must be later than start time.";
			}
		}

		if (!formData.numOrders) {
			newErrors.numOrders = "Number of orders is required.";
		} else if (parseFloat(formData.numOrders) < 0) {
			newErrors.numOrders = "Number of orders cannot be negative.";
		}

		if (!formData.earnings) {
			newErrors.earnings = "Earnings is required.";
		} else if (parseFloat(formData.earnings) < 0) {
			newErrors.earnings = "Earinings cannot be negative.";
		}

		if (!formData.bonus) {
			newErrors.bonus = "Bonus is required.";
		} else if (parseFloat(formData.earnings) < 0) {
			newErrors.bonus = "Bonus cannot be negative.";
		}

		if (!formData.distance) {
			newErrors.distance = "Distance is required.";
		} else if (parseFloat(formData.distance) < 0) {
			newErrors.distance = "Distance cannot be negative";
		}

		setErrors(newErrors);
		return Object.keys(newErrors).length === 0;
	};

	const handleSubmit = (e: FormEvent<HTMLFormElement>) => {
		e.preventDefault();

		if (validateForm()) {
			setSubmitted(true);
			console.log("Form submitted:", formData);
		}
	};

	const handleReset = () => {
		setFormData({
			date: today,
			startTime: "",
			endTime: "",
			numOrders: "",
			earnings: "",
			bonus: "",
			distance: "",
		});
		setErrors({});
		setSubmitted(false);
	};

	return (
		<form onSubmit={handleSubmit} className={styles.logForm}>
			<LogInput
				title="Date"
				id="date"
				name="date"
				type="date"
				value={formData.date}
				onChange={handleInputChange}
				error={errors.date}
			/>
			<LogInput
				title="Start Time"
				id="startTime"
				name="startTime"
				type="time"
				value={formData.startTime}
				onChange={handleInputChange}
				error={errors.startTime}
			/>
			<LogInput
				title="End Time"
				id="endTime"
				name="endTime"
				type="time"
				value={formData.endTime}
				onChange={handleInputChange}
				error={errors.endTime}
			/>
			<LogInput
				title="Number of Orders"
				id="numOrders"
				name="numOrders"
				type="number"
				value={formData.numOrders}
				onChange={handleInputChange}
				error={errors.numOrders}
				placeholder="0"
			/>
			<LogInput
				title="Earnings (DKK)"
				id="earnings"
				name="earnings"
				type="number"
				value={formData.earnings}
				onChange={handleInputChange}
				error={errors.earnings}
				placeholder="0.0"
				step={0.01}
			/>
			<LogInput
				title="Bonus (DKK)"
				id="bonus"
				name="bonus"
				type="number"
				value={formData.bonus}
				onChange={handleInputChange}
				error={errors.bonus}
				placeholder="0.0"
				step={0.01}
			/>
			<LogInput
				title="Distance (km)"
				id="distance"
				name="distance"
				type="number"
				value={formData.distance}
				onChange={handleInputChange}
				error={errors.distance}
				placeholder="0.0"
				step={0.01}
			/>

			<button type="submit" disabled={submitted}>
				Submit
			</button>

			{submitted && <button onClick={handleReset}>Add New Log</button>}
		</form>
	);
};
