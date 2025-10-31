import type { ChangeEvent } from "react";
import styles from "./LogInput.module.scss";

interface LogInputProps {
	title: string;
	id: string;
	name: string;
	type: "date" | "time" | "number";
	value: string | number;
	onChange: (e: ChangeEvent<HTMLInputElement>) => void;
	error?: string;
	placeholder?: string;
	step?: number;
}

export const LogInput = ({
	title,
	id,
	name,
	type,
	value,
	onChange,
	error,
	placeholder,
	step,
}: LogInputProps) => {
	return (
		<div className={styles.logInput}>
			<label htmlFor={id}>{title}</label>
			<input
				id={id}
				name={name}
				type={type}
				value={value}
				onChange={onChange}
				placeholder={placeholder}
				step={step}
			/>
			{error && <p style={{ color: "red", fontSize: "0.9rem" }}>{error}</p>}
		</div>
	);
};
