import React from 'react';
import {Routes, Route} from 'react-router-dom';
import MainPage from '../pages/MainPage';
import LoginPage from '../pages/LoginPage';
import RegisterPage from '../pages/RegisterPage';
import { ToastContainer } from 'react-toastify';
import AccountPage from "../pages/AccountPage.tsx";
import PrivateRoute from "../routes/PrivateRoute.tsx";
import MoviePage from "../pages/MoviePage.tsx";
import ActorPage from "../pages/ActorPage.tsx";
import PersonalizePage from "../pages/PersonalizePage.tsx";
import CreateTopicPage from "../pages/CreateTopicPage.tsx";
import DiscussionTopicPage from "../pages/DiscussionTopicPage.tsx";
import DiscussionListPage from "../pages/DiscussionListPage.tsx";

const App: React.FC = () => {
  return (
    <>
    <Routes>
      <Route path="/" element={<MainPage />} />
      <Route path="/topics" element={<PrivateRoute><DiscussionListPage /></PrivateRoute>} />
      <Route path="/login" element={<LoginPage />} />
      <Route path="/register" element={<RegisterPage/>}/>
      <Route path="/personalize" element={<PersonalizePage/>}></Route>
      <Route path="/create-topic" element={<PrivateRoute><CreateTopicPage/></PrivateRoute>} />
      <Route path="/users/:userId" element={<PrivateRoute><AccountPage/></PrivateRoute>} />
      <Route path="/movies/:movieId" element={<PrivateRoute><MoviePage/></PrivateRoute>} />
      <Route path="/actors/:actorId" element={<PrivateRoute><ActorPage/></PrivateRoute>} />
      <Route path="/topics/:topicId" element={<PrivateRoute><DiscussionTopicPage/></PrivateRoute>} />
    </Routes>
  <ToastContainer position="top-right" autoClose={3000} style={{ top: "70px" }} />
    </>);
};

export default App;