using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeMatchingEngine;
using Xunit;

namespace TestProject1
{
    public class StockMarketEngineStateMachine
    {
        private StockMarketMatchEngine sut;
        public StockMarketEngineStateMachine()
        {
            sut = new StockMarketMatchEngine();
        }

        [Fact]

        public void StockMarketEngineStateMachine_TryToChangeStateFromClosedToPreOpen_StateMustChange()
        {
            //Arrange

            //Action
            sut.PreOpen();

            //Assert

            Assert.Equal(MarcketState.PreOpen, sut.State);
        }

        [Fact]
        public void StockMarketEngineStateMachine_TryToChangeFromCloseedStateToOpened_MustThrowException()
        {
            //Arrange

            //Action


            //Assert

            Assert.Throws<NotImplementedException>(() => sut.Open());

        }
        [Fact]
        public void StockMarketEngineStateMachine_TryToChangeStateFromOpenedToPreOpened_StateMustChange()
        {
            //Arrange
            sut.PreOpen();
            sut.Open();

            //Action
            sut.PreOpen();

            //Assert

            Assert.Equal(MarcketState.PreOpen, sut.State);
        }

        [Fact]
        public void StockMarketEngineStateMachine_TryToChangeFromOpenedStateToClosed_MustThrowException()
        {
            //Arrange
            sut.PreOpen();
            sut.Open();
                
            //Action


            //Assert

            Assert.Throws<NotImplementedException>(() => sut.Close());

        }

        [Fact]
        public void StockMarketEngineStateMachine_TryToChangeStateFromPreOpenedToClosed_StateMustChange()
        {
            //Arrange
            sut.PreOpen();


            //Action
            sut.Close();

            //Assert

            Assert.Equal(MarcketState.Close, sut.State);
        }
        [Fact]
        public void StockMarketEngineStateMachine_TryToChangeStateFromPreOpenedToOpened_StateMustChange()
        {
            //Arrange
            sut.PreOpen();


            //Action
            sut.Open();

            //Assert

            Assert.Equal(MarcketState.Open, sut.State);
        }
    }
}

