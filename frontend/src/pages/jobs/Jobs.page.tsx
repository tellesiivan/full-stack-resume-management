import {useEffect, useState} from "react";
import "./jobs.scss";
import httpModule from "../../helpers/http.module";
import {Button, CircularProgress} from "@mui/material";
import {Add} from "@mui/icons-material";
import {useNavigate} from "react-router-dom";
import JobsGrid from "../../components/jobs/JobsGrid.component";
import {JobResponseDto, Response} from "../../helpers/response";

const Jobs = () => {
	const [jobs, setJobs] = useState<JobResponseDto[]>([]);
	const [loading, setLoading] = useState<boolean>(false);
	const redirect = useNavigate();

	useEffect(() => {
			setLoading(true);
			httpModule
				.get<Response<JobResponseDto[]>>("/Jobs/search")
				.then((response) => {
					const {data, errorMessage} = response.data
					if (data === undefined) throw new Error(errorMessage);
					setJobs(data);
					setLoading(false);
				})
				.catch((error) => {
					alert("Error");
					console.log(error);
					setLoading(false);
				});
		},
		[]);

	return (
		<div className="content jobs">
			<div className="heading">
				<h2>Jobs</h2>
				<Button variant="outlined" onClick={() => redirect("/jobs/add")}>
					<Add/>
				</Button>
			</div>
			{loading ? <CircularProgress size={100}/> : jobs.length === 0 ?
				<h1>No Job</h1> : <JobsGrid data={jobs}/>}
		</div>
	);
};

export default Jobs;
